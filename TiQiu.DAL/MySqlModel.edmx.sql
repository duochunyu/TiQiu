
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 01/03/2014 15:20:46
-- Generated from EDMX file: Q:\TiQiu\TiQiu.DAL\MySqlModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[tiqiu].[FK_ACCOUNT_MEMBER]', 'F') IS NOT NULL
    ALTER TABLE [tiqiu].[account] DROP CONSTRAINT [FK_ACCOUNT_MEMBER];
GO
IF OBJECT_ID(N'[tiqiu].[FK_FIELD_BUSINESSES]', 'F') IS NOT NULL
    ALTER TABLE [tiqiu].[field] DROP CONSTRAINT [FK_FIELD_BUSINESSES];
GO
IF OBJECT_ID(N'[tiqiu].[FK_FIELD_ITEM_BUSINESSES]', 'F') IS NOT NULL
    ALTER TABLE [tiqiu].[field_item] DROP CONSTRAINT [FK_FIELD_ITEM_BUSINESSES];
GO
IF OBJECT_ID(N'[tiqiu].[FK_FIELD_ITEM_FIELD]', 'F') IS NOT NULL
    ALTER TABLE [tiqiu].[field_item] DROP CONSTRAINT [FK_FIELD_ITEM_FIELD];
GO
IF OBJECT_ID(N'[tiqiu].[FK_FIELD_ORDER_LOG_FIELD_ORDER]', 'F') IS NOT NULL
    ALTER TABLE [tiqiu].[field_order_log] DROP CONSTRAINT [FK_FIELD_ORDER_LOG_FIELD_ORDER];
GO
IF OBJECT_ID(N'[tiqiu].[FK_GAME_SCHEDULED_FIELD_ORDER]', 'F') IS NOT NULL
    ALTER TABLE [tiqiu].[game_scheduled] DROP CONSTRAINT [FK_GAME_SCHEDULED_FIELD_ORDER];
GO
IF OBJECT_ID(N'[tiqiu].[FK_TEAM_MEMBER_MEMBER]', 'F') IS NOT NULL
    ALTER TABLE [tiqiu].[team_member] DROP CONSTRAINT [FK_TEAM_MEMBER_MEMBER];
GO
IF OBJECT_ID(N'[tiqiu].[FK_TEAM_MEMBER_ROLE]', 'F') IS NOT NULL
    ALTER TABLE [tiqiu].[team_member] DROP CONSTRAINT [FK_TEAM_MEMBER_ROLE];
GO
IF OBJECT_ID(N'[tiqiu].[FK_TEAM_MEMBER_TEAM]', 'F') IS NOT NULL
    ALTER TABLE [tiqiu].[team_member] DROP CONSTRAINT [FK_TEAM_MEMBER_TEAM];
GO
IF OBJECT_ID(N'[tiqiu].[FK_TEAM_SCORE_FIELD_ORDER]', 'F') IS NOT NULL
    ALTER TABLE [tiqiu].[team_score] DROP CONSTRAINT [FK_TEAM_SCORE_FIELD_ORDER];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[tiqiu].[account]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[account];
GO
IF OBJECT_ID(N'[tiqiu].[account_b]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[account_b];
GO
IF OBJECT_ID(N'[tiqiu].[account_b_businesses]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[account_b_businesses];
GO
IF OBJECT_ID(N'[tiqiu].[area]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[area];
GO
IF OBJECT_ID(N'[tiqiu].[businesses]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[businesses];
GO
IF OBJECT_ID(N'[tiqiu].[dict_team_role]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[dict_team_role];
GO
IF OBJECT_ID(N'[tiqiu].[field]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[field];
GO
IF OBJECT_ID(N'[tiqiu].[field_item]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[field_item];
GO
IF OBJECT_ID(N'[tiqiu].[field_order]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[field_order];
GO
IF OBJECT_ID(N'[tiqiu].[field_order_log]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[field_order_log];
GO
IF OBJECT_ID(N'[tiqiu].[field_rule]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[field_rule];
GO
IF OBJECT_ID(N'[tiqiu].[field_scheduled]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[field_scheduled];
GO
IF OBJECT_ID(N'[tiqiu].[file]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[file];
GO
IF OBJECT_ID(N'[tiqiu].[free_team_log]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[free_team_log];
GO
IF OBJECT_ID(N'[tiqiu].[game_scheduled]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[game_scheduled];
GO
IF OBJECT_ID(N'[tiqiu].[member]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[member];
GO
IF OBJECT_ID(N'[tiqiu].[right]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[right];
GO
IF OBJECT_ID(N'[tiqiu].[role]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[role];
GO
IF OBJECT_ID(N'[tiqiu].[role_account_b]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[role_account_b];
GO
IF OBJECT_ID(N'[tiqiu].[role_right]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[role_right];
GO
IF OBJECT_ID(N'[tiqiu].[sms_confirm]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[sms_confirm];
GO
IF OBJECT_ID(N'[tiqiu].[sms_history]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[sms_history];
GO
IF OBJECT_ID(N'[tiqiu].[sms_pool]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[sms_pool];
GO
IF OBJECT_ID(N'[tiqiu].[team]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[team];
GO
IF OBJECT_ID(N'[tiqiu].[team_member]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[team_member];
GO
IF OBJECT_ID(N'[tiqiu].[team_score]', 'U') IS NOT NULL
    DROP TABLE [tiqiu].[team_score];
GO
IF OBJECT_ID(N'[tiqiuModelStoreContainer].[v_field_item_scheduled]', 'U') IS NOT NULL
    DROP TABLE [tiqiuModelStoreContainer].[v_field_item_scheduled];
GO
IF OBJECT_ID(N'[tiqiuModelStoreContainer].[v_field_order]', 'U') IS NOT NULL
    DROP TABLE [tiqiuModelStoreContainer].[v_field_order];
GO
IF OBJECT_ID(N'[tiqiuModelStoreContainer].[v_field_scheduled]', 'U') IS NOT NULL
    DROP TABLE [tiqiuModelStoreContainer].[v_field_scheduled];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'account_b'
CREATE TABLE [dbo].[account_b] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [NAME] varchar(50)  NOT NULL,
    [PWD] char(60)  NOT NULL,
    [STATUS] int  NOT NULL,
    [CREATE_TIME] datetime  NOT NULL,
    [LAST_LOGIN_TIME] datetime  NULL
);
GO

-- Creating table 'account_b_businesses'
CREATE TABLE [dbo].[account_b_businesses] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [BUSINESSESS_ID] int  NOT NULL,
    [ACCOUNT_B_ID] int  NOT NULL
);
GO

-- Creating table 'businesses'
CREATE TABLE [dbo].[businesses] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [LEVEL] int  NOT NULL,
    [STATUS] int  NOT NULL,
    [NAME] varchar(200)  NOT NULL,
    [BRIEF] varchar(1000)  NULL
);
GO

-- Creating table 'dict_team_role'
CREATE TABLE [dbo].[dict_team_role] (
    [ID] int  NOT NULL,
    [NAME] varchar(100)  NULL,
    [TYPE] int  NULL
);
GO

-- Creating table 'field_scheduled'
CREATE TABLE [dbo].[field_scheduled] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FIELD_ID] int  NOT NULL,
    [FIELD_ITEM_ID] int  NOT NULL,
    [SCHEDULED_DATE] datetime  NOT NULL,
    [START_TIME] time  NOT NULL,
    [END_TIME] time  NOT NULL,
    [PRICE] decimal(8,2)  NOT NULL,
    [STATUS] int  NOT NULL,
    [REMARK] varchar(200)  NULL,
    [RULE_TYPE] int  NOT NULL
);
GO

-- Creating table 'game_scheduled'
CREATE TABLE [dbo].[game_scheduled] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TEAM_A_ID] int  NOT NULL,
    [TEAM_B_ID] int  NOT NULL,
    [START_TIME] datetime  NOT NULL,
    [END_TIME] datetime  NOT NULL,
    [FIELD_ID] int  NULL,
    [GROUP_CODE] char(10)  NULL,
    [FIELD_ITEM_ID] int  NULL,
    [FIELD_ORDER_ID] int  NULL
);
GO

-- Creating table 'rights'
CREATE TABLE [dbo].[rights] (
    [ID] int  NOT NULL,
    [NAME] varchar(45)  NULL,
    [CODE] varchar(45)  NULL,
    [TYPE] varchar(45)  NULL
);
GO

-- Creating table 'roles'
CREATE TABLE [dbo].[roles] (
    [ID] int  NOT NULL,
    [NAME] varchar(45)  NOT NULL,
    [DESCRIPTION] varchar(45)  NULL
);
GO

-- Creating table 'role_account_b'
CREATE TABLE [dbo].[role_account_b] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ACCOUNT_B_ID] int  NULL,
    [BUSINESSESS_ID] int  NULL,
    [ROLE_ID] int  NULL,
    [FIELD_ID] int  NULL
);
GO

-- Creating table 'role_right'
CREATE TABLE [dbo].[role_right] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ROLE_ID] int  NULL,
    [RIGHT_ID] int  NULL
);
GO

-- Creating table 'sms_confirm'
CREATE TABLE [dbo].[sms_confirm] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SMS_POOL_ID] int  NOT NULL,
    [CELL_PHONE] char(11)  NOT NULL,
    [CONTENT] varchar(200)  NOT NULL,
    [RECEIVE_DATE] datetime  NOT NULL
);
GO

-- Creating table 'sms_history'
CREATE TABLE [dbo].[sms_history] (
    [SMS_POOL_ID] int  NOT NULL,
    [FIELD_ORDER_ID] int  NOT NULL,
    [MEMBER_ID] int  NULL,
    [BUSINESSES_ID] int  NULL,
    [CELL_PHONE] char(11)  NOT NULL,
    [CONTENT] varchar(200)  NOT NULL,
    [CREATE_DATE] datetime  NOT NULL,
    [SEND_DATE] datetime  NULL
);
GO

-- Creating table 'sms_pool'
CREATE TABLE [dbo].[sms_pool] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FIELD_ORDER_ID] int  NOT NULL,
    [MEMBER_ID] int  NULL,
    [BUSINESSES_ID] int  NULL,
    [CELL_PHONE] char(11)  NOT NULL,
    [CONTENT] varchar(200)  NOT NULL,
    [CREATE_DATE] datetime  NOT NULL,
    [LAST_SEND_DATE] datetime  NULL
);
GO

-- Creating table 'teams'
CREATE TABLE [dbo].[teams] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [NAME] varchar(200)  NOT NULL,
    [LEVEL] int  NOT NULL,
    [SCORE] int  NOT NULL,
    [WIN] int  NOT NULL,
    [LOSE] int  NOT NULL,
    [DRAW] int  NOT NULL,
    [BRIEF] varchar(1000)  NULL,
    [BUILD_DATE] datetime  NOT NULL
);
GO

-- Creating table 'team_member'
CREATE TABLE [dbo].[team_member] (
    [ID] int  NOT NULL,
    [MEMBER_ID] int  NOT NULL,
    [TEAM_ID] int  NOT NULL,
    [ROLE_ID] int  NOT NULL,
    [STATUS] int  NOT NULL
);
GO

-- Creating table 'field_item'
CREATE TABLE [dbo].[field_item] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FIELD_ID] int  NOT NULL,
    [BUSINESSES_ID] int  NOT NULL,
    [LEVEL] int  NOT NULL,
    [TYPE] int  NOT NULL,
    [BRIEF] varchar(500)  NOT NULL,
    [STATUS] int  NOT NULL,
    [NAME] varchar(45)  NOT NULL
);
GO

-- Creating table 'field_rule'
CREATE TABLE [dbo].[field_rule] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FIELD_ID] int  NOT NULL,
    [FIELD_ITEM_ID] int  NOT NULL,
    [TYPE] int  NOT NULL,
    [FIELD_TYPE] int  NOT NULL,
    [SCHEDULE_TYPE] int  NOT NULL,
    [START_DATE] datetime  NOT NULL,
    [END_DATE] datetime  NOT NULL,
    [START_TIME] time  NOT NULL,
    [END_TIME] time  NOT NULL,
    [RULE_NAME] varchar(200)  NOT NULL,
    [DESCRIPTION] varchar(500)  NULL,
    [PRICE] decimal(8,2)  NOT NULL,
    [STATUS] int  NOT NULL
);
GO

-- Creating table 'v_field_scheduled'
CREATE TABLE [dbo].[v_field_scheduled] (
    [ID] int  NOT NULL,
    [FIELD_ID] int  NOT NULL,
    [FIELD_ITEM_ID] int  NOT NULL,
    [SCHEDULED_DATE] datetime  NOT NULL,
    [START_TIME] time  NOT NULL,
    [END_TIME] time  NOT NULL,
    [PRICE] decimal(8,2)  NOT NULL,
    [STATUS] int  NOT NULL,
    [REMARK] varchar(200)  NULL,
    [RULE_TYPE] int  NOT NULL,
    [NAME] varchar(200)  NOT NULL,
    [INCOME] decimal(8,2)  NULL,
    [ORDER_STATUS] int  NOT NULL
);
GO

-- Creating table 'file'
CREATE TABLE [dbo].[file] (
    [ID] int  NOT NULL,
    [TYPE] int  NOT NULL,
    [FK_ID] int  NOT NULL,
    [FILE_NAME] varchar(200)  NOT NULL,
    [FILE_EXT] varchar(5)  NOT NULL,
    [ORDER] int  NOT NULL,
    [SIZE] int  NULL,
    [UPLOAD_MEMBER_ID] int  NULL,
    [UPLOAD_DATE] datetime  NULL,
    [PATH] varchar(200)  NOT NULL,
    [TITLE] varchar(50)  NULL
);
GO

-- Creating table 'area'
CREATE TABLE [dbo].[area] (
    [CODE] char(6)  NOT NULL,
    [AREA_NAME] varchar(45)  NOT NULL
);
GO

-- Creating table 'v_field_order'
CREATE TABLE [dbo].[v_field_order] (
    [ID] int  NOT NULL,
    [FIELD_SCHEDULED_ID] int  NOT NULL,
    [FIELD_ID] int  NOT NULL,
    [FIELD_ITEM_ID] int  NOT NULL,
    [ORDER_DATE] datetime  NOT NULL,
    [NEED_REFEREE] bit  NOT NULL,
    [STATUS] int  NOT NULL,
    [START_TIME] time  NOT NULL,
    [END_TIME] time  NOT NULL,
    [MEMBER_ID] int  NOT NULL,
    [CREATE_DATE] datetime  NOT NULL,
    [INCOME] decimal(8,2)  NULL,
    [SCORE] int  NULL,
    [REMARK] varchar(200)  NULL,
    [MEMBERB_ID] int  NULL,
    [FIELD_NAME] varchar(200)  NOT NULL,
    [FIELD_ITEM_TYPE] int  NOT NULL,
    [FIELD_ITEM_NAME] varchar(45)  NOT NULL,
    [MEMBER_NAME] varchar(50)  NOT NULL,
    [BUSINESSES_ID] int  NOT NULL,
    [TYPE] varchar(2)  NOT NULL
);
GO

-- Creating table 'field_order'
CREATE TABLE [dbo].[field_order] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FIELD_SCHEDULED_ID] int  NOT NULL,
    [FIELD_ID] int  NOT NULL,
    [FIELD_ITEM_ID] int  NOT NULL,
    [ORDER_DATE] datetime  NOT NULL,
    [NEED_REFEREE] bit  NOT NULL,
    [STATUS] int  NOT NULL,
    [START_TIME] time  NOT NULL,
    [END_TIME] time  NOT NULL,
    [MEMBER_ID] int  NOT NULL,
    [EXPIRE_DATE] datetime  NOT NULL,
    [CREATE_DATE] datetime  NOT NULL,
    [TYPE] int  NOT NULL,
    [PK_PAY_TYPE] int  NULL,
    [INCOME] decimal(8,2)  NULL,
    [SCORE] int  NULL,
    [REMARK] varchar(200)  NULL,
    [MEMBERB_ID] int  NULL,
    [PRICE] decimal(8,2)  NULL,
    [PRICE_UNIT] int  NULL,
    [FREE_TEAM_MIN_PLAYER] int  NULL
);
GO

-- Creating table 'free_team_log'
CREATE TABLE [dbo].[free_team_log] (
    [ID] int  NOT NULL,
    [FIELD_ORDER_ID] int  NOT NULL,
    [MEMBER_ID] int  NOT NULL,
    [CHECKIN_DATE] datetime  NOT NULL,
    [PLAYER_COUNT] int  NOT NULL,
    [STATUS] int  NOT NULL,
    [CONFIRM_DATE] datetime  NULL
);
GO

-- Creating table 'field_order_log'
CREATE TABLE [dbo].[field_order_log] (
    [ID] int  NOT NULL,
    [FIELD_ORDER_ID] int  NOT NULL,
    [FIELD_ITEM_ID] int  NOT NULL,
    [OPERATION] varchar(200)  NOT NULL,
    [MEMBER_ID] int  NULL,
    [ACCOUNT_B_ID] int  NULL,
    [LOG_DATE] datetime  NOT NULL
);
GO

-- Creating table 'team_score'
CREATE TABLE [dbo].[team_score] (
    [FIELD_ORDER_ID] int  NOT NULL,
    [TEAM_A_ID] int  NOT NULL,
    [TEAM_B_ID] int  NULL,
    [TEAM_A_SCORE] int  NOT NULL,
    [TEAM_A_NAME] varchar(200)  NOT NULL,
    [TEAM_B_NAME] varchar(200)  NULL,
    [START_TIME] datetime  NOT NULL,
    [END_TIME] datetime  NOT NULL,
    [SCORE] varchar(45)  NULL,
    [RECEIVE_SCORE] int  NULL,
    [LOSE_SCORE] int  NULL,
    [TEAM_A_COLOR] varchar(20)  NULL,
    [TEAM_B_COLOR] varchar(20)  NULL
);
GO

-- Creating table 'fields'
CREATE TABLE [dbo].[fields] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [BUSINESSES_ID] int  NOT NULL,
    [AREA_CODE] char(6)  NOT NULL,
    [L] float  NULL,
    [B] float  NULL,
    [NAME] varchar(200)  NOT NULL,
    [BRIEF] varchar(500)  NULL,
    [LEVEL] int  NULL,
    [ADRESS] varchar(500)  NOT NULL,
    [SCORE] int  NOT NULL,
    [BOTTOM_PRICE] decimal(8,2)  NOT NULL,
    [TOP_PRICE] decimal(8,2)  NOT NULL,
    [HAS_BATHROOM] bit  NOT NULL,
    [STATUS] int  NOT NULL,
    [TEL] varchar(15)  NULL,
    [PHONE] varchar(15)  NULL,
    [PIC_PATH] varchar(200)  NULL
);
GO

-- Creating table 'v_field_item_scheduled'
CREATE TABLE [dbo].[v_field_item_scheduled] (
    [ID] int  NOT NULL,
    [BUSINESSES_ID] int  NOT NULL,
    [AREA_CODE] char(6)  NOT NULL,
    [L] float  NULL,
    [B] float  NULL,
    [NAME] varchar(200)  NOT NULL,
    [BRIEF] varchar(500)  NULL,
    [LEVEL] int  NULL,
    [ADRESS] varchar(500)  NOT NULL,
    [SCORE] int  NOT NULL,
    [HAS_BATHROOM] bit  NOT NULL,
    [BOTTOM_PRICE] decimal(8,2)  NOT NULL,
    [TOP_PRICE] decimal(8,2)  NOT NULL,
    [STATUS] int  NOT NULL,
    [TEL] varchar(15)  NULL,
    [PHONE] varchar(15)  NULL,
    [PIC_PATH] varchar(200)  NULL,
    [FIELD_ITEM_ID] int  NOT NULL,
    [FIELD_ITEM_STATUS] int  NOT NULL,
    [FIELD_ITEM_NAME] varchar(45)  NOT NULL,
    [TYPE] int  NOT NULL,
    [SCHEDULED_ID] int  NOT NULL,
    [SCHEDULED_DATE] datetime  NOT NULL,
    [START_TIME] time  NOT NULL,
    [END_TIME] time  NOT NULL,
    [PRICE] decimal(8,2)  NOT NULL,
    [SCHEDULED_STATUS] int  NOT NULL
);
GO

-- Creating table 'accounts'
CREATE TABLE [dbo].[accounts] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [NAME] varchar(50)  NOT NULL,
    [PWD] char(60)  NOT NULL,
    [STATUS] int  NOT NULL,
    [CREATE_DATE] datetime  NOT NULL,
    [LAST_LOGIN_DATE] datetime  NULL,
    [MEMBER_ID] int  NOT NULL
);
GO

-- Creating table 'members'
CREATE TABLE [dbo].[members] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [NAME] varchar(50)  NOT NULL,
    [IS_TEAMLEADER] bit  NOT NULL,
    [CELLPHONE] char(11)  NOT NULL,
    [EMAIL] varchar(50)  NULL,
    [ADRESS] varchar(500)  NULL,
    [AREA_CODE] char(6)  NULL,
    [LEVLE] int  NOT NULL,
    [SCORE] int  NULL,
    [NICK_NAME] varchar(50)  NULL,
    [POSITION] varchar(10)  NULL,
    [FAV_FOOT] int  NULL,
    [FAV_TEAM] varchar(40)  NULL,
    [FAV_STAR] varchar(20)  NULL,
    [BRIEF] varchar(200)  NULL,
    [Work] varchar(100)  NULL,
    [Intro] varchar(1000)  NULL,
    [Brithday] datetime  NULL,
    [PalyingAge] int  NULL,
    [Feature] varchar(1000)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'account_b'
ALTER TABLE [dbo].[account_b]
ADD CONSTRAINT [PK_account_b]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'account_b_businesses'
ALTER TABLE [dbo].[account_b_businesses]
ADD CONSTRAINT [PK_account_b_businesses]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'businesses'
ALTER TABLE [dbo].[businesses]
ADD CONSTRAINT [PK_businesses]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'dict_team_role'
ALTER TABLE [dbo].[dict_team_role]
ADD CONSTRAINT [PK_dict_team_role]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'field_scheduled'
ALTER TABLE [dbo].[field_scheduled]
ADD CONSTRAINT [PK_field_scheduled]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'game_scheduled'
ALTER TABLE [dbo].[game_scheduled]
ADD CONSTRAINT [PK_game_scheduled]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'rights'
ALTER TABLE [dbo].[rights]
ADD CONSTRAINT [PK_rights]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'roles'
ALTER TABLE [dbo].[roles]
ADD CONSTRAINT [PK_roles]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'role_account_b'
ALTER TABLE [dbo].[role_account_b]
ADD CONSTRAINT [PK_role_account_b]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'role_right'
ALTER TABLE [dbo].[role_right]
ADD CONSTRAINT [PK_role_right]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'sms_confirm'
ALTER TABLE [dbo].[sms_confirm]
ADD CONSTRAINT [PK_sms_confirm]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [SMS_POOL_ID] in table 'sms_history'
ALTER TABLE [dbo].[sms_history]
ADD CONSTRAINT [PK_sms_history]
    PRIMARY KEY CLUSTERED ([SMS_POOL_ID] ASC);
GO

-- Creating primary key on [ID] in table 'sms_pool'
ALTER TABLE [dbo].[sms_pool]
ADD CONSTRAINT [PK_sms_pool]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'teams'
ALTER TABLE [dbo].[teams]
ADD CONSTRAINT [PK_teams]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'team_member'
ALTER TABLE [dbo].[team_member]
ADD CONSTRAINT [PK_team_member]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'field_item'
ALTER TABLE [dbo].[field_item]
ADD CONSTRAINT [PK_field_item]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'field_rule'
ALTER TABLE [dbo].[field_rule]
ADD CONSTRAINT [PK_field_rule]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID], [RULE_TYPE], [NAME], [ORDER_STATUS] in table 'v_field_scheduled'
ALTER TABLE [dbo].[v_field_scheduled]
ADD CONSTRAINT [PK_v_field_scheduled]
    PRIMARY KEY CLUSTERED ([ID], [RULE_TYPE], [NAME], [ORDER_STATUS] ASC);
GO

-- Creating primary key on [ID] in table 'file'
ALTER TABLE [dbo].[file]
ADD CONSTRAINT [PK_file]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [CODE] in table 'area'
ALTER TABLE [dbo].[area]
ADD CONSTRAINT [PK_area]
    PRIMARY KEY CLUSTERED ([CODE] ASC);
GO

-- Creating primary key on [ID], [FIELD_SCHEDULED_ID], [FIELD_ID], [FIELD_ITEM_ID], [ORDER_DATE], [NEED_REFEREE], [STATUS], [START_TIME], [END_TIME], [MEMBER_ID], [CREATE_DATE], [FIELD_NAME], [FIELD_ITEM_TYPE], [FIELD_ITEM_NAME], [MEMBER_NAME], [BUSINESSES_ID], [TYPE] in table 'v_field_order'
ALTER TABLE [dbo].[v_field_order]
ADD CONSTRAINT [PK_v_field_order]
    PRIMARY KEY CLUSTERED ([ID], [FIELD_SCHEDULED_ID], [FIELD_ID], [FIELD_ITEM_ID], [ORDER_DATE], [NEED_REFEREE], [STATUS], [START_TIME], [END_TIME], [MEMBER_ID], [CREATE_DATE], [FIELD_NAME], [FIELD_ITEM_TYPE], [FIELD_ITEM_NAME], [MEMBER_NAME], [BUSINESSES_ID], [TYPE] ASC);
GO

-- Creating primary key on [ID] in table 'field_order'
ALTER TABLE [dbo].[field_order]
ADD CONSTRAINT [PK_field_order]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'free_team_log'
ALTER TABLE [dbo].[free_team_log]
ADD CONSTRAINT [PK_free_team_log]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'field_order_log'
ALTER TABLE [dbo].[field_order_log]
ADD CONSTRAINT [PK_field_order_log]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [FIELD_ORDER_ID] in table 'team_score'
ALTER TABLE [dbo].[team_score]
ADD CONSTRAINT [PK_team_score]
    PRIMARY KEY CLUSTERED ([FIELD_ORDER_ID] ASC);
GO

-- Creating primary key on [ID] in table 'fields'
ALTER TABLE [dbo].[fields]
ADD CONSTRAINT [PK_fields]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID], [BUSINESSES_ID], [AREA_CODE], [NAME], [ADRESS], [SCORE], [HAS_BATHROOM], [BOTTOM_PRICE], [TOP_PRICE], [STATUS], [FIELD_ITEM_ID], [FIELD_ITEM_STATUS], [FIELD_ITEM_NAME], [TYPE], [SCHEDULED_ID], [SCHEDULED_DATE], [START_TIME], [END_TIME], [PRICE], [SCHEDULED_STATUS] in table 'v_field_item_scheduled'
ALTER TABLE [dbo].[v_field_item_scheduled]
ADD CONSTRAINT [PK_v_field_item_scheduled]
    PRIMARY KEY CLUSTERED ([ID], [BUSINESSES_ID], [AREA_CODE], [NAME], [ADRESS], [SCORE], [HAS_BATHROOM], [BOTTOM_PRICE], [TOP_PRICE], [STATUS], [FIELD_ITEM_ID], [FIELD_ITEM_STATUS], [FIELD_ITEM_NAME], [TYPE], [SCHEDULED_ID], [SCHEDULED_DATE], [START_TIME], [END_TIME], [PRICE], [SCHEDULED_STATUS] ASC);
GO

-- Creating primary key on [ID] in table 'accounts'
ALTER TABLE [dbo].[accounts]
ADD CONSTRAINT [PK_accounts]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'members'
ALTER TABLE [dbo].[members]
ADD CONSTRAINT [PK_members]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ROLE_ID] in table 'team_member'
ALTER TABLE [dbo].[team_member]
ADD CONSTRAINT [FK_TEAM_MEMBER_ROLE]
    FOREIGN KEY ([ROLE_ID])
    REFERENCES [dbo].[dict_team_role]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TEAM_MEMBER_ROLE'
CREATE INDEX [IX_FK_TEAM_MEMBER_ROLE]
ON [dbo].[team_member]
    ([ROLE_ID]);
GO

-- Creating foreign key on [TEAM_ID] in table 'team_member'
ALTER TABLE [dbo].[team_member]
ADD CONSTRAINT [FK_TEAM_MEMBER_TEAM]
    FOREIGN KEY ([TEAM_ID])
    REFERENCES [dbo].[teams]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TEAM_MEMBER_TEAM'
CREATE INDEX [IX_FK_TEAM_MEMBER_TEAM]
ON [dbo].[team_member]
    ([TEAM_ID]);
GO

-- Creating foreign key on [BUSINESSES_ID] in table 'field_item'
ALTER TABLE [dbo].[field_item]
ADD CONSTRAINT [FK_FIELD_ITEM_BUSINESSES]
    FOREIGN KEY ([BUSINESSES_ID])
    REFERENCES [dbo].[businesses]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FIELD_ITEM_BUSINESSES'
CREATE INDEX [IX_FK_FIELD_ITEM_BUSINESSES]
ON [dbo].[field_item]
    ([BUSINESSES_ID]);
GO

-- Creating foreign key on [FIELD_ORDER_ID] in table 'game_scheduled'
ALTER TABLE [dbo].[game_scheduled]
ADD CONSTRAINT [FK_GAME_SCHEDULED_FIELD_ORDER]
    FOREIGN KEY ([FIELD_ORDER_ID])
    REFERENCES [dbo].[field_order]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GAME_SCHEDULED_FIELD_ORDER'
CREATE INDEX [IX_FK_GAME_SCHEDULED_FIELD_ORDER]
ON [dbo].[game_scheduled]
    ([FIELD_ORDER_ID]);
GO

-- Creating foreign key on [FIELD_ORDER_ID] in table 'field_order_log'
ALTER TABLE [dbo].[field_order_log]
ADD CONSTRAINT [FK_FIELD_ORDER_LOG_FIELD_ORDER]
    FOREIGN KEY ([FIELD_ORDER_ID])
    REFERENCES [dbo].[field_order]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FIELD_ORDER_LOG_FIELD_ORDER'
CREATE INDEX [IX_FK_FIELD_ORDER_LOG_FIELD_ORDER]
ON [dbo].[field_order_log]
    ([FIELD_ORDER_ID]);
GO

-- Creating foreign key on [FIELD_ORDER_ID] in table 'team_score'
ALTER TABLE [dbo].[team_score]
ADD CONSTRAINT [FK_TEAM_SCORE_FIELD_ORDER]
    FOREIGN KEY ([FIELD_ORDER_ID])
    REFERENCES [dbo].[field_order]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [BUSINESSES_ID] in table 'fields'
ALTER TABLE [dbo].[fields]
ADD CONSTRAINT [FK_FIELD_BUSINESSES]
    FOREIGN KEY ([BUSINESSES_ID])
    REFERENCES [dbo].[businesses]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FIELD_BUSINESSES'
CREATE INDEX [IX_FK_FIELD_BUSINESSES]
ON [dbo].[fields]
    ([BUSINESSES_ID]);
GO

-- Creating foreign key on [FIELD_ID] in table 'field_item'
ALTER TABLE [dbo].[field_item]
ADD CONSTRAINT [FK_FIELD_ITEM_FIELD]
    FOREIGN KEY ([FIELD_ID])
    REFERENCES [dbo].[fields]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FIELD_ITEM_FIELD'
CREATE INDEX [IX_FK_FIELD_ITEM_FIELD]
ON [dbo].[field_item]
    ([FIELD_ID]);
GO

-- Creating foreign key on [MEMBER_ID] in table 'accounts'
ALTER TABLE [dbo].[accounts]
ADD CONSTRAINT [FK_ACCOUNT_MEMBER]
    FOREIGN KEY ([MEMBER_ID])
    REFERENCES [dbo].[members]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ACCOUNT_MEMBER'
CREATE INDEX [IX_FK_ACCOUNT_MEMBER]
ON [dbo].[accounts]
    ([MEMBER_ID]);
GO

-- Creating foreign key on [MEMBER_ID] in table 'team_member'
ALTER TABLE [dbo].[team_member]
ADD CONSTRAINT [FK_TEAM_MEMBER_MEMBER]
    FOREIGN KEY ([MEMBER_ID])
    REFERENCES [dbo].[members]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TEAM_MEMBER_MEMBER'
CREATE INDEX [IX_FK_TEAM_MEMBER_MEMBER]
ON [dbo].[team_member]
    ([MEMBER_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------