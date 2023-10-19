ALTER TABLE [dbo].[Notifications] ADD [FollowedWriterNewArticleId] [int] NOT NULL DEFAULT 0
ALTER TABLE [dbo].[Notifications] ADD [FollowedWriterNewArticleTitle] [nvarchar](max)
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202307220641196_UpdateNotificationNewArticle', N'IslandOfHealing.Migrations.Configuration',  0x1F8B0800000000000400ED5D5F6F1CC7917F0F90EFB0D8A72450B8A4140377021943A2A41C13536244D9C99B30DC6D5203CFCEF06666251241804B905C7C671C70389CED38306007F043122397BB0B90384E1C7F1949D45BBE427A7666FA6FF5BF999ED95981E00BB7A7BBBABAFBD755D5DDD55D7FFBF317DBAF9ECDA3D16394666112EF8CB73636C723144F9359189FEC8C17F9F1D7FF61FCEA37BFFCA5EDDBB3F9D9E88D3ADFB5221F2E19673BE347797E7A7D32C9A68FD03CC836E6E1344DB2E438DF9826F349304B26573737FF71B2B5354198C418D31A8DB6EF2FE23C9CA3E50FFC733789A7E8345F04D17E32435156A5E32F874BAAA3BBC11C65A7C114ED8CF7B2288867F78EFF0905116672A32C311EDD88C200737388A2E3F12888E3240F72CCEBF5D7337498A7497C72788A1382E8C1F929C2F98E832843551BAED3ECB6CDD9BC5A3467420BD6A4A68B2C4FE68E04B7AE55FD33118B37EAE531E93FDC83B7714FE7E745AB97BDB833BEB1B78FB22C38C1AD176BBBBE1BA5454E552F6F90B25746428E2B041C1843C5DF95D1EE22CA1729DA89D1224F83E8CAE860711485D3EFA0F307C99B28DE891751C4B28A99C5DFB8049C749026A728CDCFEFA3E3AA017BB3F168C2979B88054931A64CD5B238BF76753CBA8B2B0F8E224490C0F4C2619EA4E85B28466990A3D94190E7288D0B1A68D99752ED425D1871A9B93E338DBBE1F4CDE2574D09C31877F478B41F9CBD86E293FCD1CEF81B781ADE09CFD0AC4EA888BF1E87780EE33279BA408D2AFFEE026525009595E37FBBA8FDC61EA913CFE58DE2A733851B71F604A5FDF3BE1787F92D0C9ABAE6E2FF07E1DC82D0DDE07178B2849F4072FFBC188EF1E83E8A969FB347E169D535C58787644A621978274DE6F793A81A42E6D3C307417A8272CC56027F3F4C16E954E06A7B42658756A2940C3A0B93A2D8A51CD1C0783A4DB09EEC7DFA1F0459F6244967BD577C1844BAD6763467FD8959836C989FBC9E461EDAA7AFA698DF9A4AB65EE9A20F6F8669FE68169CCB724F5F6EFFFC00CFFEFEC7FCDBC9D18330F7D151A67E493A1FF06F2D42DD54F5534973C5A6102E656137B47C2F0DB104C5892729D65B9C95207E72E4EA3E16D24FF6D1FC08EB1EAC5F6BD237133C990A7CBA1B50D48AB13400950600ABE0DB1901B592571901B59160CD599A87D348C157F5F1616DBF50B6F82F9269227C862C131D53B7CF4E519C2998AA3E024CF15F24A684CFAE4CDD49A22879528214E68CCB01F0077D97B80433B9F27A2F9DA9985C7E029863D325A6B88FADECCC0A194DD6AD65C94B6B535D97491F7AB181769338475A93D68FBADA4D1EA3B40F6BEB80DA3C4D95452D4577F1B09D24E979DBAD83C3C57C1EA4E79AA65FF331940F8293ACFBEE85747D632DEFCD88F1B1C3839B82A678A23AC3C7A48E7793F91CCF31AD56AEF260A94C64AAA49FA53C2A4D2D67745539BB586FE1CE782D7C53A1B7990C30CF6006896138972BB7A42499B2BAAE26991E527349EA6B3993AAB3819CEEFC2B7794AC8D36D19454D874B62C1D04E1AC461356B66F667BF171027228E5C45532692CCB86AC520F9BF2FBB05DAAB9D2DC84A9085C5A32EABA481F2B95D3D5CD4EF6AFBCE997D770C16C38AAAA829E3319B304F5AFA3148249ADCC1ACD6646913498CA4CE9CB793C4CE88A137239D0EDECFC828697295DC1A72D3F1519EFC755BAA9DDD08E1327B5DEDA6BA39F1993AEA98626242EE7B6BA2EC3814B37A7141D9CCEDA6C81365D0128549966ADD008F992D9DB00FA128D4BECABEB5AF6507BD5C6F4795B529DB82D70ABB26E5772E24CB15DF9359A2FD5514083595295BC9C1B9AB991C4B8BDD37CAF7FEF031FE6E64170EEC5C4BB31673D3F9ACB87B63662BFFE4CD6A773E27C571CDE359ADEC2519DF324E7CA5F4E75755D6547B59F7365577FEF51721355246783D4874AD8373AFA15A780F67CB8D144589EDB369800CB7297C057D7B58FD2E9A320CE971D7557E720D48DA2C326116674259EC565D5B7E741A83B9AEDB2EE834749AC6BF7D561EF0C174ADD0BA1DBB11F3A85FFDE4AA054547C908653D44ED47BB1FA301BB8D105473E1675FE4E84F1AA87B2A6F5E8E1F2C98E3DCC6749F340795CCFFF34DAD1CAE748C5537B2DC8758CFBBE082D7DA911D575AD4480DC4141D1D93A2796AD577C78CA78905237C3A880CCEEF954EBA335AC5B2DEE67E34BE7B0BDF9499BE3F19AC6E57CD3D4E5EB2A82A7D3B815ACC6AC0EA06B2C19CEA0E56CEA6368206F2BFD7437C9C3E3705A5D16759E366CF1CB19A3AECB8FC75F3C6B4F851DB11A4E74B3B480AF2247FB7AFA1718F5864EB9AF71173DF174E0A1A26B7282F6749926BB8F8261EECDB2A35EE21594906CB68770292A278D992569692EE17181215426AE3380CF267EDBAF3AA00E6D25DB4B2297125E23EFF46B90AE4CEB155962DE4DFAC59C35E8F7C6186977A2E084BEB4D1E0AD096F70C59D85D11F9D63122C9CF81E2AAFFF552D78F6F1BBCF7FFEE1C5DBEF8D476F04D102A76C4A5DCA17F8E32F5FBCF59B17FFF2535260CB50E02FEFBCF8E3EF9E7EF61929201BC25C818BF77F7CF1FE7F3DFBF41352E09A3C38E530688646BC28D97A9878822B1CB2E71FFCE6E2BF7FFFE293B7ED87ECFF850286212B733FFBF8FF9EBFF39EF5A82DCB3C7FEB3F9FFDFB87AD06CEE3900D60B05EFCC7BF5DFCEA0BEB91FAEB4F9FFDEED7CF3FFAD47A722D733FFDF4B7B6C354161086F69A4D196168BFD1606815067BEB7106E9AE7286FEE8B7CFDEFDE0E9E71FE13EBBF8F1674F3FFFE0D9FFFCC11603B8D88B2F3E7FF1A78F2F7EF193B2E4C5FB7F79F1CB5F3DFDEC5F9FBFFBBFCFDFFDD9C5271FA9E0018FC28D2C4BA6E1B27798551E7B979A67E7763C1B192E56D345586946EEE38E0C4F71D7619D5970259A49F7E25B2842391ADD98968F45ED06D93498C9EA1B376066CB0DB9D742B961DE7CE259FA9A5413B6D8505A984C41843193E1F10FE35C36EFC2781A9E0691BE438462967661D15C5281F8E5163A2D4CD938D737DEA6E67A4D2FD74E2A1106C1D437DB1306537AA8292F39A846D97CE381196EE1B29169CC2D6A8140556F897588F246683435A207589A46CB860566AF632510859DF555C83178EE53D8707767EC9169B8E1B906B0D4B6A0074C6A47681D00A9F6A73788339D73BD041CE60A482FEADBE20AB011DC9ED4B8B1BBFA939CEAEE70402AF7A6C22A215BEF2B1A3020ED2F3618782551004D9D1BA96D5028F0DD1FF4845118BC2969BA48A1C287F5AD0A0A18E0AE903D1EADEFE3AF812EB76C4B0FA0B51C451B4E841B452B01B37007408525D585000A1D724DC71EA0AA37C0862E3061BE7BC01E3C0A831798A0C3BD0A147AEF7B0A0DE1DE883DEAF42FBB0D1D7B3AEE7B40A06E746CAA575C1E59092C215F5C1568B48EB91433D51D0E7B30EA9C79593B80F58A1D1A24356DE801919A91B1D2C1BC2FF94A81A8958BB0FFB507E8AD97FC83B8EE11656BA577757E95C60D699D9325B0F34D5D891B6C7E6B9C34CD8B93CD8D0DF974B2DD3EB69A9F3EB7B2D523B00E9B87669735152C1CFCD760274C170CDABBBE292AABDDC5862628AD1BD603A4AD47D48617EAD63B045C6B75B6D697D13B76D74B916B98EF1F910356EBA50360A10D7009E29DB1D40E67D0BB9698B5CA4F25AB7C1545F814F40E514E7C07E953F6D4D950F6639050C893297A04A25082CF5098BED32473502B7D3B12F4315E152572486F20C8BF912B51E34E552D9B579D8E84BA8632A76106AAC0DEB04414C863A04A9FF39788911D3E038972619F93AD198990B075632057BF8F2F91A9563CA66EA20B3CB087D895B52DC66ACB4C0B336A111BC8B2B208A2C8AB070762A5A63491AC0D188130238EF859CE05BF607229E2638812D2ECD5459A44A48A2466CDCE580C11965F5197F1ADB4E801F563A97257D8F91CC14B2295D711DB2E51E469BAC9E86724D3F5D15B8AF727E5AEB2F07DE1DAA3F77E611AC38B714D0FE9FD5DBAE91ECD83854A3499BC31A061D7F863000D63D59419521A3F8A4E3B8D5C1D52F614689D434D104D730BBE756480E68372AC41DB8D6FF8C99DE1745ACD35CBF6BC9A69A7D1DE70AAA05B1C89AF9FC95DA73B1BE51AA2381D65F8A66695A63F14E7A11DE0087E024BEE01F3C99DFDD91DD30CD142D4F489F6B4AE839E011F68913BC67876647D7AC4B4A1B67435DDA13B2F62E72167F47AEB14154CD40718E6238C66EDEF1A06DA270934969E69A7DD79AF1DB2F798E5878DC9A7D95DEF46B25ADC5696BBD071BFB8E18E31D36061D9A5E949FB3D620579B210F3DDB5AA0969DCA4B4DEA6F4D161BE266B7D1B966C86916FDB9332707D95B03D5144B8DFDE0F4E4FB1606422DE5729A3C332DCFDEED70FDD63C0CF4B1A9329678B8B5B77A4A63C49F16A54F85AAC4066E84E982E0391044741B1D3B23B9B03D9CAAD3FC53E405D8BB4BB270F57BD29501729FEAF5F4CD087A30736492B227770D38A25E6B295085A85CB6571E9C369100529701D1D2FCB16F358BDE7AB2E5DEFD1B21454FBB67A2A3462B1488B7E71A348A3BD8B14E9177B8AC5F56F964EF1DBA5741DBD9DA751A7DA53A297DCB95123A932A5ED89001B693F5D42A97418C1E3DE6A569482C7C3848004ABC55C808B75330D4854736E74EB447B3A344A394B88A6DA532AC38EB354CA147B0AF0846C3219EBF720B8FEADD2ECA99431C0591A658A3D051AD09BA54253ED29D521BE593A759A3D151AB09BA543535D5A96888D4A5CCA97E1B65902654A9762C93407CA973BA0790085D5D651149F8760698ADF1CF02886D7E6A0297E74525DA2AEA9920623DA359BA2EEE60EBC82B331765425BB91F1C054759EA7E4CD31960A4974A1538702E609D5A92EB3ED5C9C644E9210BA94C629418B4B6B6AEA24EC2FA7C8EA4487B15BC6F5E5862E8056F69A7E02A54713B9E14F52FA31BE69CC5EAE9349EAD0A40E398AF3277C6A7785C6324849A01B5144DC2BF8D90FFA5C68B9F0064536B8274B8B4DEF1BD88C73262091605A2B82367780EA01D7ACE38C3BA8B5A587BC8FD064C495885E3A1D715806DC904C14801941525DE67B151C939FEF55A2331D9929EEC36066057056EE4FE6531FB0C6525F43A29B59022CC75D97E2EBB27B241F727B187A23518BB1B7A0D191D2A71116B9A94B935DAC7EEE52346FFF6BEF4BBF04D0226E021E1055BB6BBAE34859B22B9391C62014179F757ADFEA9A041614D69FAEEAB10E2CC8E9FCB9FBDEABB80756A6BC843340700DF1300F786F63F7D96028DFCD9C6083F5B154D874D75D46E9D6B5BCD968BC98FD1220ACF232F180ACD201DD1D518A72DD20498A7EC71D0C881F9D743EAAA3DA094A9FA4BB52AB02D5C9E4AA0FAEF4AAE07332BDEAC36ACE10EA8872A23C77A54442CAB18448A2034724A41CC7114975A354455F124955C9BDEB71FE7A3FBF41AABDF9AF1F413B6DBCAA050AEBFDE7636DA27672B45995E84A0F75314A839571DA97A4BA21509C0FAE73810F47C61FA6B25F5E426D0D383DFADC5C2797B45AECAFAB6974836E3F3E0B3E3724D7064DBC0BA307207177F3DC31A42F3EE4FD6C7A0B9F3F9A53DDCD575352BCC6CF496F384BAB3A8C353458AE8171B380E51B98AF7D6D801B8021ABC314AF626871B8ABD25E7241517B6F7B1617B057BBA3D0501119AA5DE547770D0C46925FBA9885D45EA590DFC42FBDF209E79CD597AD2F5CCF97ADCE2AFF74D149BCCC321EE12E7A1CCE0A07F1C3F32C47F38D22C3C6E13F47E58A9766D80FE2F0186579195A647C7573EBEA7874230A83ACBC3C50B9BF5F175F96B0F287DFBA56F8C3A3D97C221677F7AA2FA864D98C8B7E0604B385FDCAFB08921616DD6A0C83D62A40FAB20AE98D8F3D3CE9CF76C63F5816B93EDAFBFEC3B2D495D17223E7FA6873F4C346155317DAB2FAF871506C10A55278E806B4A9673B4FFB2BF3E0ECABEEA16DF7B82E722E5D7BB7FBE0450CEE36C3FFE71EE335036F0CAD2BBA89E3B95F7C513774BF744BC7741F186930B3AC429B72F10C952CDA902A5DD635DC6DBDD2208E7AEDC0AE9E1936BCD50EEC3E46823AB15BB7D586C5A55BBB876128BDDB3D106A2895D493ABF46D6F3790A24F7B73112EB9B197A48E427752950FBB2D2FD6721B7E6B735D45B779D2B88B2F21D0752BBC5317730FC40EA8CC6A8228C8B5DCCDA09329B432EE8863BA66F8AE390F5FE9A9EEA3BB3D08046FF26E651638756CB7459EAB28029DC1D7562211B7720DA8F162727548625DCCFD83B2994C6A2C8A9CC06D0D4CA543F7DAA272CD460A40EDD299BCB9F2A36EE42D314FDCC89BF3C2798F77B33AD77B5CAF2D8C8D8BD506CBC1AE374A0C0ED06B3B168C237573A922784FBB4916AE702BE9D2350640E7E5F51D79C607DAFF96EC4AAC5CE245DD523BD46ED4ED66441BE5D23594359EC76B0B68D681B9F9C829BC95DDB00C1219B46C031C87D7160892EBB15FF1C6BA217741B9F248EE8274E59CAC5DD4AE6E4D4B3D955B12229ECA6D19228ECA9ECF94A8D7727341B53A2DCB7B373B5A7B6CE196CADE45C5DA1BFACA907DEB2A0EBB40307557D62EE39CB77D5BCF0ADE6B791D8EE00DCEBE6B0B3BCBF3E3156F5B7578DC698D00B5ABEEDA0EFE0A0F3A6A3761B7AAEB72AD2A573813379766A0EFB08F09A5F3166ECEAEC125D887E741E5173CDC95B5311AE1BA4E69C88C68A8513D6A06CFC32907E5D3061D12A205F51B1B8F798F9DC652631860524D81FF1A85D953BC506B03E69153D452CDD3D07265C3088BAB0DCB24BFA46F138CB62BEC08B1EB6446C8A74E50A47E09D33F904CEFDEC935AE3AD4AD45F0AA41A1898B5DC870C1A5AF3B8EF4EFCC0D0F4496E1BD3401B82044311FFB145436E0F60B29DD9B695DC928175C695F99ED1360A698C52B35917A474F6FE6910362566A1C39858B1B94560362CF31BC005FD75DC3D93C1928576A7844AF1790E902EB0D44149160C74CF5246DAD4591FA5DC081892273F0C18160458867CD30217C596BDC98DED1932BB47C53AE1730190336B29A848DFDC8EA1036BD17645521CE191EAA944E90A47FE7C93BA054CFE7013A4BFF5457AF001AAC14EA172B7D491D7B900C634FD11400B499C9BCB9B1B1258D9B26D427BC4348BFAEBBE96BF37ED7F0B6781C439C2A026AD621549911863EF72273B8437115471DC1CDEA2D1DEFC8333CF825D7A77E126B15981BACF65A2592FAD265CED8E951A5092F1891930D3124AD387ED5A348B584A5A799F4E8563EE92CDF2CDA19CF8E0ABFDFF2FC9739BD95151D5F4B094BA9823219A20D071A9698AF75AFCC7AFD05645C15385B419F9CD4A9AA211934B5914853A64AB9331DA946EE2B541D17FDC7B281CC9983AA894C165D9732D1554C7503FB7B52E5401EA876209BA172B231245549BE401591680F26FAC23682548BF01DAA4BC862A8B05A39481555E95005D5B3EAC67162D7D0F208B15FC1B1619F36B69E70D4F055CF399A473BEDE833B4A6DA79C52555CC7F86EAD4C65BD75657DB80DA4AEB4CA6AA55C1EBC1F8F4A2230E109B5EEBAB03D823A4A17582A40D411F1BA618932A5A24CA58F3AA062A9D478096DA399AC06B4296FD3A4DD77050B7407100DB7701ECEE00B4DFC22FC24BE3011D27058BF336F2C001BD7AE84DA7F950F325F509C7F5B240834D4736EF887A35A16E3DB8DE683BC33B6D9AE9F41368ABD381A917B82B4D1D4510B0F6DD229CD701BDA03BD16B39E28271C545A86ADF34F0900968A0F930AA6533411B0F0840D4BEC9D05108D062E38989FACC8485239BAE693E6771325172FC355735B2EA8DFD9623DA5D9374BBCD3A3BC4B439EDD71411AD6E45D484F6DD61DE57053AC57133D6B81DCBB411FAACE93068A1204707F0DE4DAAF960DC316C392D7C37577A7D9B7CDB9E946B9B2A01FF945ED9DE9EDC5FC4C55584F2D72D94852794C436A619A329B75746F2ECC5C749BD652770546791AEBCE7C12CC883620E1C07D31C7F9EE28509168FE3D11B41B42894DDFC08CDF6E27B8BFC7491E326A3F951C4D97AC5D69FAEFEED89C4F3F6BDD3E512D24713309B61717BE35E7C73114633C2F71DE0F6868244B1A758DD7829C6322F6EBE9C9C134A77A5C85C2A4255F791ADD007687E1A152FAEDD8B0F83C7A8096F18BCAFA193608A955EF958BA9A887920F86EDFBE1506276930CF2A1AB43CFE89313C9B9F7DF3EF56472B1F210C0100 , N'6.1.3-40302')
