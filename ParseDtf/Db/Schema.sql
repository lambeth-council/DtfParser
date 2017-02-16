
/****** Object:  Table [dbo].[RecordIdentifier]    Script Date: 02/16/2017 12:27:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecordIdentifier](
	[RecordIdentifierId] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_RecordType] PRIMARY KEY CLUSTERED 
(
	[RecordIdentifierId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LPI]    Script Date: 02/16/2017 12:27:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LPI](
	[UPRN] [bigint] NULL,
	[LPI_KEY] [nvarchar](max) NULL,
	[LANGUAGE] [nvarchar](max) NULL,
	[LOGICAL_STATUS] [bigint] NULL,
	[START_DATE] [date] NULL,
	[END_DATE] [date] NULL,
	[ENTRY_DATE] [date] NULL,
	[LAST_UPDATE_DATE] [date] NULL,
	[SAO_START_NUMBER] [bigint] NULL,
	[SAO_START_SUFFIX] [nvarchar](max) NULL,
	[SAO_END_NUMBER] [bigint] NULL,
	[SAO_END_SUFFIX] [nvarchar](max) NULL,
	[SAO_TEXT] [nvarchar](max) NULL,
	[PAO_START_NUMBER] [bigint] NULL,
	[PAO_START_SUFFIX] [nvarchar](max) NULL,
	[PAO_END_NUMBER] [bigint] NULL,
	[PAO_END_SUFFIX] [nvarchar](max) NULL,
	[PAO_TEXT] [nvarchar](max) NULL,
	[USRN] [bigint] NULL,
	[LEVEL] [nvarchar](max) NULL,
	[POSTAL_ADDRESS] [nvarchar](max) NULL,
	[POSTCODE] [nvarchar](max) NULL,
	[POST_TOWN] [nvarchar](max) NULL,
	[OFFICIAL_FLAG] [nvarchar](max) NULL,
	[CUSTODIAN_ONE] [bigint] NULL,
	[CUSTODIAN_TWO] [bigint] NULL,
	[CAN_KEY] [nvarchar](max) NULL,
	[VersionId] [int] NOT NULL,
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_LPI] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_LPI] ON [dbo].[LPI] 
(
	[UPRN] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_LPI_1] ON [dbo].[LPI] 
(
	[USRN] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DtfLine]    Script Date: 02/16/2017 12:27:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DtfLine](
	[RecordIdentifier] [int] NULL,
	[ChangeType] [nvarchar](max) NULL,
	[ProOrder] [int] NULL,
	[TextLine] [nvarchar](max) NULL,
	[VersionId] [int] NULL,
	[FieldCount] [int] NULL,
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Line] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BLPU]    Script Date: 02/16/2017 12:27:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BLPU](
	[UPRN] [bigint] NULL,
	[LOGICAL_STATUS] [bigint] NULL,
	[BLPU_STATE] [bigint] NULL,
	[BLPU_STATE_DATE] [date] NULL,
	[BLPU_CLASS] [nvarchar](max) NULL,
	[PARENT_UPRN] [bigint] NULL,
	[X_COORDINATE] [decimal](12, 2) NULL,
	[Y_COORDINATE] [decimal](12, 2) NULL,
	[RPC] [bigint] NULL,
	[LOCAL_CUSTODIAN_CODE] [bigint] NULL,
	[START_DATE] [date] NULL,
	[END_DATE] [date] NULL,
	[LAST_UPDATE_DATE] [date] NULL,
	[ENTRY_DATE] [date] NULL,
	[ORGANISATION] [nvarchar](max) NULL,
	[WARD_CODE] [nvarchar](max) NULL,
	[PARISH_CODE] [nvarchar](max) NULL,
	[CUSTODIAN_ONE] [bigint] NULL,
	[CUSTODIAN_TWO] [bigint] NULL,
	[CAN_KEY] [nvarchar](max) NULL,
	[VersionId] [int] NOT NULL,
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_BLPU] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_BLPU] ON [dbo].[BLPU] 
(
	[UPRN] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Version]    Script Date: 02/16/2017 12:27:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Version](
	[VersionId] [int] IDENTITY(1,1) NOT NULL,
	[StartDateTime] [datetime] NOT NULL,
	[EndDateTime] [datetime] NULL,
	[SourceFilename] [varchar](max) NOT NULL,
	[Messages] [varchar](max) NULL,
 CONSTRAINT [PK_Version] PRIMARY KEY CLUSTERED 
(
	[VersionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StreetRecord]    Script Date: 02/16/2017 12:27:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StreetRecord](
	[USRN] [bigint] NULL,
	[RECORD_TYPE] [bigint] NULL,
	[SWA_ORG_REF_NAMING] [bigint] NULL,
	[STATE] [bigint] NULL,
	[STATE_DATE] [date] NULL,
	[STREET_SURFACE] [bigint] NULL,
	[STREET_CLASSIFICATION] [bigint] NULL,
	[VERSION] [bigint] NULL,
	[RECORD_ENTRY_DATE] [date] NULL,
	[LAST_UPDATE_DATE] [date] NULL,
	[STREET_START_DATE] [date] NULL,
	[STREET_END_DATE] [date] NULL,
	[STREET_START_X] [decimal](12, 2) NULL,
	[STREET_START_Y] [decimal](12, 2) NULL,
	[STREET_END_X] [decimal](12, 2) NULL,
	[STREET_END_Y] [decimal](12, 2) NULL,
	[STREET_TOLERANCE] [bigint] NULL,
	[VersionId] [int] NOT NULL,
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_StreetRecord] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StreetDescriptor]    Script Date: 02/16/2017 12:27:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StreetDescriptor](
	[USRN] [bigint] NULL,
	[STREET_DESCRIPTOR] [nvarchar](max) NULL,
	[LOCALITY_NAME] [nvarchar](max) NULL,
	[TOWN_NAME] [nvarchar](max) NULL,
	[ADMINSTRATIVE_AREA] [nvarchar](max) NULL,
	[LANGUAGE] [nvarchar](max) NULL,
	[VersionId] [int] NOT NULL,
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_StreetDescriptor] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[spTruncateEverything]    Script Date: 02/16/2017 12:27:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spTruncateEverything]
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	truncate table DtfLine
	truncate table [Version]

	truncate table StreetRecord
	truncate table StreetDescriptor
	truncate table BLPU
	truncate table LPI
	
	
END
GO
/****** Object:  View [dbo].[vwLpi]    Script Date: 02/16/2017 12:27:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwLpi]
AS
SELECT     LTRIM(ISNULL(SAO_TEXT, '')) AS SaoDesc, LTRIM(CASE WHEN SAO_START_NUMBER IS NULL THEN '' ELSE ' ' + CAST(SAO_START_NUMBER AS nvarchar) 
                      + isnull(SAO_START_SUFFIX, '') END + CASE WHEN SAO_END_NUMBER IS NULL THEN '' ELSE '-' + CAST(SAO_END_NUMBER AS nvarchar) 
                      + isnull(SAO_END_SUFFIX, '') END) AS SaoNumber, LTRIM(ISNULL(PAO_TEXT, '')) AS PaoDesc, LTRIM(CASE WHEN PAO_START_NUMBER IS NULL 
                      THEN '' ELSE ' ' + CAST(PAO_START_NUMBER AS nvarchar) + isnull(PAO_START_SUFFIX, '') END + CASE WHEN PAO_END_NUMBER IS NULL 
                      THEN '' ELSE '-' + CAST(PAO_END_NUMBER AS nvarchar) + isnull(PAO_END_SUFFIX, '') END) AS PaoNumber, UPRN, LPI_KEY, LOGICAL_STATUS, USRN, POSTCODE,
                       POST_TOWN
FROM         dbo.LPI
GO
/****** Object:  View [dbo].[vwDtfLine]    Script Date: 02/16/2017 12:27:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwDtfLine]
AS
SELECT     dbo.DtfLine.RecordIdentifier, dbo.DtfLine.ChangeType,dbo.DtfLine.ProOrder, dbo.DtfLine.TextLine, dbo.DtfLine.FieldCount, dbo.DtfLine.EntityId, dbo.DtfLine.VersionId, dbo.RecordIdentifier.Description as RecordIdentifierDescription, 
                      dbo.Version.StartDateTime AS LoadDate, dbo.Version.SourceFilename
FROM         dbo.DtfLine LEFT OUTER JOIN
                      dbo.RecordIdentifier ON dbo.DtfLine.RecordIdentifier = dbo.RecordIdentifier.RecordIdentifierId LEFT OUTER JOIN
                      dbo.Version ON dbo.DtfLine.VersionId = dbo.Version.VersionId
GO
/****** Object:  View [dbo].[vwAddresses]    Script Date: 02/16/2017 12:27:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwAddresses]
AS
SELECT     dbo.vwLpi.UPRN, dbo.vwLpi.SaoDesc, dbo.vwLpi.SaoNumber, 
                      dbo.vwLpi.PaoDesc, dbo.vwLpi.PaoNumber, dbo.StreetDescriptor.STREET_DESCRIPTOR AS Street, 
                      dbo.vwLpi.POST_TOWN AS PostTown, dbo.vwLpi.POSTCODE, 
                      dbo.vwLpi.LPI_KEY, dbo.vwLpi.LOGICAL_STATUS AS LpiStatus, dbo.BLPU.LOGICAL_STATUS AS BlpuStatus, 
                      dbo.BLPU.BLPU_CLASS AS BlpuClass, dbo.BLPU.PARENT_UPRN AS ParentUprn, dbo.BLPU.X_COORDINATE AS Easting, 
                      dbo.BLPU.Y_COORDINATE AS Northing
                      
                       ,CASE WHEN BLPU_Class = 'PS' THEN 5 ELSE 0 END + CASE WHEN BLPU_Class = 'P' THEN 4 ELSE 0 END + CASE WHEN BLPU_Class LIKE 'X%' THEN 3 ELSE 0 END
                       + CASE WHEN BLPU_Class LIKE 'C%' THEN 2 ELSE 0 END + CASE WHEN (BLPU_Class LIKE 'R0%' OR
                      BLPU_Class LIKE 'RD%' OR
                      BLPU_Class LIKE 'RH%' OR
                      BLPU_Class LIKE 'RI%') THEN 1 ELSE 0 END AS BlpuClassId, vwLpi.LOGICAL_STATUS AS StatusId
                      
                      
FROM         dbo.vwLpi LEFT OUTER JOIN
                      dbo.BLPU ON dbo.vwLpi.UPRN = dbo.BLPU.UPRN LEFT OUTER JOIN
                      dbo.StreetDescriptor ON dbo.vwLpi.USRN = dbo.StreetDescriptor.USRN
GO
