using System;
using System.Globalization;
using CLib;
using Common.Economat.Entity;
using DataService.Properties;

namespace DataService.ViewModel.Economat
{

    /// <summary>
    /// SchoolFeeWrapper
    /// </summary>
    public class SchoolFeeCard
    {
        /// <summary>
        /// SchoolFeeWrapper
        /// </summary>
        /// <param name="schoolFee"></param>
        public SchoolFeeCard(SchoolFee schoolFee)
        {
            SchoolFeeGuid     = schoolFee.SchoolFeeGuid;
            Designation       = schoolFee.Designation;

            if (schoolFee.IsPaid)
            {
                NumeroReference = "Ref: " + schoolFee.NumeroReference;
                Observations = schoolFee.NetAmount.ToString(CultureInfo.CurrentCulture)
                               + " dhs" + " Payer par "
                               + schoolFee.IsPaidBy + " a Finance User le "
                               + schoolFee.DatePaid.GetValueOrDefault().ToShortDateString()
                               + " par " + schoolFee.PaymentMethode.GetEnumDescription();
                YesNoImage = ImagesHelper.ImageToByteArray(Resources.yes);
            }
            else
            {
                Observations = schoolFee.NetAmount.ToString(CultureInfo.CurrentCulture) + " dhs";
                YesNoImage = ImagesHelper.ImageToByteArray(Resources.No);
            }
        }


        /// <summary>
        /// SchoolFeeGuid
        /// </summary>
        public Guid SchoolFeeGuid { get;  }


        /// <summary>
        ///YesNoImage
        /// </summary>
        public byte[] YesNoImage { get; set; }


        /// <summary>
        /// Motif de ce Payement, exp: Oct-2015
        /// </summary>
        public string Designation { get; }


        /// <summary>
        /// Numero de Reference de Recue
        /// </summary>
        public string NumeroReference { get; }


        /// <summary>
        /// Observations
        /// </summary>
        public string Observations { get; set; }

    }
}
