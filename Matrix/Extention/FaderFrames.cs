/* Copyright (c) 2007, Dr. WPF
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 *   * Redistributions of source code must retain the above copyright
 *     notice, this list of conditions and the following disclaimer.
 * 
 *   * Redistributions in binary form must reproduce the above copyright
 *     notice, this list of conditions and the following disclaimer in the
 *     documentation and/or other materials provided with the distribution.
 * 
 *   * The name Dr. WPF may not be used to endorse or promote products
 *     derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY Dr. WPF ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL Dr. WPF BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Matrix.Extention
{
    public class FaderFrame : Frame
    {
        #region FadeDuration

        public static readonly DependencyProperty FadeDurationProperty =
            DependencyProperty.Register("FadeDuration", typeof(Duration), typeof(FaderFrame),
                new FrameworkPropertyMetadata(new Duration(TimeSpan.FromMilliseconds(300))));

        /// <summary>
        /// FadeDuration will be used as the duration for Fade Out and Fade In animations
        /// </summary>
        public Duration FadeDuration
        {
            get { return (Duration)GetValue(FadeDurationProperty); }
            set { SetValue(FadeDurationProperty, value); }
        }

        #endregion

        public FaderFrame()
            : base()
        {
            // watch for navigations
            Navigating += OnNavigating;
        }

        public override void OnApplyTemplate()
        {
            // get a reference to the frame's content presenter
            // this is the element we will fade in and out
            _contentPresenter = GetTemplateChild("PART_FrameCP") as ContentPresenter;
            base.OnApplyTemplate();
        }

        protected void OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            // if we did not internally initiate the navigation:
            //   1. cancel the navigation,
            //   2. cache the target,
            //   3. disable hittesting during the fade, and
            //   4. fade out the current content
            if (Content != null && !_allowDirectNavigation && _contentPresenter != null)
            {
                e.Cancel = true;
                _navArgs = e;
                _contentPresenter.IsHitTestVisible = false;
                var da = new DoubleAnimation(0.0d, FadeDuration) {DecelerationRatio = 1.0d};
                da.Completed += FadeOutCompleted;
                _contentPresenter.BeginAnimation(OpacityProperty, da);
            }
            _allowDirectNavigation = false;
        }

        private void FadeOutCompleted(object sender, EventArgs e)
        {
            // after the fade out
            //   1. re-enable hittesting
            //   2. initiate the delayed navigation
            //   3. invoke the FadeIn animation at Loaded priority
            ((AnimationClock) sender).Completed -= FadeOutCompleted;
            if (_contentPresenter == null) return;
            _contentPresenter.IsHitTestVisible = true;

            _allowDirectNavigation = true;
            switch (_navArgs.NavigationMode)
            {
                case NavigationMode.New:
                    if (_navArgs.Uri == null)
                    {
                        NavigationService.Navigate(_navArgs.Content);
                    }
                    else
                    {
                        NavigationService.Navigate(_navArgs.Uri);
                    }
                    break;

                case NavigationMode.Back:
                    NavigationService.GoBack();
                    break;

                case NavigationMode.Forward:
                    NavigationService.GoForward();
                    break;

                case NavigationMode.Refresh:
                    NavigationService.Refresh();
                    break;
            }

            Dispatcher.BeginInvoke(DispatcherPriority.Loaded,
                (ThreadStart)delegate ()
                {
                    var da = new DoubleAnimation(1.0d, FadeDuration) {AccelerationRatio = 1.0d};
                    _contentPresenter.BeginAnimation(OpacityProperty, da);
                });
        }

        private bool _allowDirectNavigation;
        private ContentPresenter _contentPresenter;
        private NavigatingCancelEventArgs _navArgs;
    }
}
