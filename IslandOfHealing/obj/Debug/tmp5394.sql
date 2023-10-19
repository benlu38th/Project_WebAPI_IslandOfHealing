CREATE TABLE [dbo].[CollectLikes] (
    [Id] [int] NOT NULL IDENTITY,
    [UserId] [int] NOT NULL,
    [ArticleId] [int] NOT NULL,
    [Like] [bit] NOT NULL,
    [Collect] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.CollectLikes] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_UserId] ON [dbo].[CollectLikes]([UserId])
CREATE INDEX [IX_ArticleId] ON [dbo].[CollectLikes]([ArticleId])
ALTER TABLE [dbo].[CollectLikes] ADD CONSTRAINT [FK_dbo.CollectLikes_dbo.Articles_ArticleId] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[Articles] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[CollectLikes] ADD CONSTRAINT [FK_dbo.CollectLikes_dbo.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202307071104325_CreateCollectlike', N'IslandOfHealing.Migrations.Configuration',  0x1F8B0800000000000400ED5DCD8FE43815BF23F13F443901EAEDEA8F418256F5AE7A6AA6771BA63F34D5B3E2367225AEEA68F251244ED325C471A50509890BE208070E704088DB4A48EC3FC30CDCF817B0F3E1D889EDD8A954D2331A8D34EA76EC67FBBDDFFBB0F35EFA7FFFFC76FAD943E05BF7304EBC283CB50FF70F6C0B864EE47AE1EAD44ED1F2931FD99F7DFADDEF4C9FBBC183F565D9EF98F4C323C3E4D4BE43687D329924CE1D0C40B21F784E1C25D112ED3B5130016E34393A38F8F1E4F0700231091BD3B2ACE9CB34445E00B35FF0AFB32874E01AA5C0BF8C5CE827453B7E32CFA85A572080C91A38F0D4BE487C10BAD7CB2F20F0F122F7F311B675E67B00AF660EFDA56D81308C104078AD27AF1238477114AEE66BDC00FCDBCD1AE27E4BE027B0D8C349D55D773B0747643B936A6049CA4913140586040F8F0BFE4CEAC33B71D9A6FCC31C7C8E398D3664D719174FEDB318798E0F675110C010D9567DCA93991F93EE3256EFF304F6AC5AB73D0A138C26F26FCF9AA53E4A63781AC214C5C0DFB36ED285EF393F859BDBE80D0C4FC3D4F7D945E365E3675C036EBA89A3358CD1E6255C165BB9706D6BC28F9BD407D261CC98627B213A3EB2AD2B3C3958F890628261C51C4531FC1C86300608BA37002118878406CCB8DA98BD3617E5713E21C621E68F6D5D828717305CA1BB531B4BCDB6CEBD07E8962DC5225E851ED6423C08C529142C523DF145E8A16778C9E5CCE4E75BAC70C684B0F6C4ED1C53D328F0624CE60ADC7BAB4C0E3582979B82A46DBD847ED623B9F3D6B9F297F07CCDC3145B88F3380A5E467EB5A27A97D7B7205E4122AE48DD6F1EA5B1535BF07452699A8EFE7557BC8F1A279FEBD6433E54E8DB133D756B53EB10A9D51AFFD8CB3CD831BF8AFD9D4F740336E51C4F238C7C107655F26486C5B68AE2CDB646639E060188378AAD1FF721CA5BB04A76CFDE385AC530A9262266AA6A1CCBB6BFF0DEC0AB34D84E4EB3C8F7A183B6A6D3C9D1483D44C3F6F7EB274AFBDFE6274A7F62ECD8A81AA9564E3B95F38AD6DEEC24F372829E223FA75E3F11A370CDE481709DDC83C6DAF8A77DF85D86B55D1D3025F1D113CBE722FF2B2CEBE10F1F57DCDB664A9436A4AB264AEC8842673B213F574A63B493611F112E9FEBCC71A25419066A469B8620BF0149F28B2876079F780EFC3E825EC359AF3CE74D8B31E925AABF085643C4DA44D307B78B4FBD18DDB955945FD945F5B8CBCD8D4F0E0343CBFC27D1A2ED24A7C9A836BE443B17F8E7A9A752D57E26E9ED50501A977C700B5ABA394DCD50B0EE1CC581622787581C5AC801A8835F64467F748FF2B9C6BC3A149C74B7BDE028A46E4CA6E32D2603B2D74CCF4A59841D1A4A23EE657E2E959EEBF809F26EB245661AAC5E61D6C54CB7D380D1ECEA56E52239F7C1AA7AD363ACE625A9DE741C5B7417C6FE069360C1CCCBE012060B1817BBF9EF6F7FFD9FBF7C6B5B5F023FC5BF1E3424C6F57EFBAFAFDEFEFDAFEFFEF40D1D70D83220EBFDEF6FFE46071CE90C78FBE77FBCFBFD1FE898639D31EFBEFEDDDBDFFC918E79D2146E2E46B6F12C4922C7CB84C59B83C6950D3FFDF3D0B534EF6FAAEB38AA61975894DE1A0B0F838D70B06E88AFC367D087085A674EFE7E6F061207B84DC5C77B724D1746AF3F1A0BA3AF92F8F5FDA0312D76103026161AF833ACA9188E5E889ADEC40B1D6F0D7C3D46D5866BBA23C2033A51FDC933B88621F1247A1CD15901E32C9A0BA1F3D584D4C6AEE98481A1163A0597012D3050DD0C3480C0DC5F0D0955D53D62AB16F58B5239BB86C3A99C1D0648E55E968C02593E1497414012975762CFDDFE2070945C170F0541212B06809D70DB3AF39651F828F01207B132D1B644B49588B9E35B9B98DBE8EB43A8574477429F720703A05029A1F7C1450B4E2B7A68E18F2E3D43913BF40C69567B0021BBF6C111C84AE59119C3FCB44A3245F0087A18CA32471E4449777869C58134296EFCEAD821F4E610C9DE7057C763C9C1A10146214105256D124558E3A98831616C0B55223411A15C3B5A06337811D1E0D4B7468A116C638BCDA401A6774B7E411D7BFAC755BA39565A0D2CEB9F329BF4AAFDD46D07CF0D7D4E895E8D4A79D57674323C3C09F8C542B39D738A438F862C3A30AD7635DE64942260D708D99945175AA5E08138D2DECDBE25B79DCDFD6B4494063125B319DE5028D8A28E2287604F6EF8DA78D30C6E74C39BEDB9C205346DA053F0A3BCF4A56E943E9B4EF2CA80A2613A9194104C2FC17AED852BA6A4A068B1E6793DC1EC93B979927D90D398389C0ED69D3E9D09453158C1DA53C234179E7B7142DEDC810520B7D433371074CB830689A72B6711C7054D91952EB01C477E2E62339D7C7F41A055503AC79B243DB2FD42A9736912B0489D07F0412C785B85819506A13C76948FA6810F4B42120D2957415FAD726BA1ADFA94CAF093A5230B49E5549833144B4871B422FA51935123006EE0A27176E0E16602C63E51D81D7E43E1AE485C6009144D26D82D32CC79EC168D2674CA0C729E50D9AA4F294B116789640DC6A8E5AE3C05F0555E89CAA9D36C7196246D34905D960ECE892E6B31E0137D01C9318BB68E617768A2374B88369AA0A9CAF5E6F154B5EFCA1A8E6CC19873437FB6AC3A0377366A0A12BBB16E79261E3B3E6FD925AE47927D1EADF6206E5128AE2162F1B0DD8895E6B27236B96C347113656E2AEF2BCA5603939E259B72F63C6B31802A4D1CE5E04A5B0D205BA48672FC2DDAF4A9E4999F2C8DBC459F4295C6C952A95AF52995899D2C9DB24D9F4A95A6C9D2A95A4D7616D53715998CCF932C590279CB18EE964FA114E981289932A3378EA1E34EF93DD83BF656D5DCEC2947EFC6FA8D773053856CCD78AD43B0268CD406C75EE322A7DE85CE4E2F746A1737D3E212A5FD03118D5B95BC8B6D6106DD7B2EB951996F1204837DD2617FFE737FE67BD9B9AAEC7009426F091394E7EED947078747B5EF4B3C9E6F3D4C92C4F5059750F20F3EF0521B20D5D823BC6D4D26364EC7E53EAD10DE83D8B90371F3E30A5B5690B9F867B4FD9713321E6C9BFCEC093FD77111BAF0E1D4FE6536EAC4BAF8D96B3A70CFBA8E319E4FAC03EB572DD31B7FBEE0C3801157652204D1130E431D3E0F5052FD5E001EBE6F4E8BFF04C056C49832FF85D7198F82127F436032140C10DA5C50ED1B0142F11D1B8B8FFD24C076ECAE95FD77B101BD99A35AB17F97B5344BFDBB5011D8450DE4E4A37668CF241729EFAD61CB8FB80AC520F573C380D1AC4EF9C3607FAD3658C3B3742AFDED8B2E5BD92BB5799DAA757BF2A97C71EE5666992DC0ED4B33EAF5B522CDD0591B5F6FBB9D24EA35B41A7B352C99DD4A0C6C59EC56847A7391A2D2573D41762A3CFD300CDD40EE7CE4D391389EDA2698AE9593B6939061ACB72AB9F1CBE1EA9F4B1CBAFC6DC0FA217932CDE34B9ADFBEAEED11D5B18D58AE3620BC3ED42AB4B1AACE86078FE49DF1A880798F6AC9C6F7695C6941D78AA1C7EECDD46FFA1E9F2B33A8FF1AD3DC8C879DA1CC8E29701E41E5563D57BB2E3C557D96467956FE96129F3817E47C9D1F03DAAA722493AA4AB814D368D36742378DF22ED58CF2721C611998B40A4C3487B8D8405120D6561F269A445520315E0599A80E45A7DEA9865651F2FE7B5818A62A0413244D7563E7D8C55E8D3A9B1137B6B36AAEAEF016287923D5E8515669994BB6DFAD1A146035F373B00F66FECC0B0E01126F5591207FF425840EE77D699F8B70199541406D456597FACD3944C0C5AE99C061091C841F3B3049B2AF2D161F9D7A1E2CA07B115EA7689D22BC65182C7C4EFF4930A19A3FAB32E3D73CBD5E671F44EB630B78991EB96FBE0E9FA69EEFD2759F0B2E0325244894525CEE12592272C9BBDA504A5751A849A8601F0DAE6E61B0F631B1E43A9C837BD8656D18BA2FE00A389B32CD4A4EA45D103CDBA7CF3CB08A41901434AAF1F8578C613778F8F4FFD1651C7DED680000 , N'6.1.3-40302')

