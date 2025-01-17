﻿CREATE TABLE [dbo].[STUDENTS] (
    [GUID]                  UNIQUEIDENTIFIER NOT NULL,
    [MATRICULE]             TEXT             NULL,
    [NOM]                   TEXT             NOT NULL,
    [PRENOM]                TEXT             NOT NULL,
    [CIVILITE]              TEXT             NULL,
    [PHOTO_IDENTITE]        IMAGE            NULL,
    [NUMERO_ID]             TEXT             NULL,
    [TYPE_ID]               TEXT             NULL,
    [DATE_NAISSANCE]        DATE             NULL,
    [NATIONALITE]           TEXT             NULL,
    [LIEU_NAISSANCE]        TEXT             NULL,
    [NUMERO_TEL]            TEXT             NULL,
    [ADRESS_EMAIL]          TEXT             NULL,
    [ADRESS_DOMICILE]       TEXT             NULL,
    [NOM_TUTEUR]            TEXT             NULL,
    [PRENOM_TUTEUR]         TEXT             NULL,
    [NUMERO_TEL_TUTEUR]     TEXT             NULL,
    [ADRESS_EMAIL_TUTEUR]   TEXT             NULL,
    [ADRESS_DOMICIL_TUTEUR] TEXT             NULL,
    [STATUT]                TEXT             NULL,
    [DATE_REGISTRATION]     DATE             NULL,
    PRIMARY KEY CLUSTERED ([GUID] ASC)
);


