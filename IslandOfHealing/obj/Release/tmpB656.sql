EXECUTE sp_rename @objname = N'dbo.Articles.ArticlesClassId', @newname = N'ArticlesCategoryId', @objtype = N'COLUMN'
EXECUTE sp_rename @objname = N'dbo.Articles.IX_ArticlesClassId', @newname = N'IX_ArticlesCategoryId', @objtype = N'INDEX'
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202307051347165_AdjustArticle3', N'IslandOfHealing.Migrations.Configuration',  0x1F8B0800000000000400ED5B4F6F1C3514BF23F11D467302946692B44810EDB64A37092C347FD44D2B6E9577C6BBB1EAB197194FC80A71440224242E88231C38C001216E9590E897A1851B5F017BFE783C339E59CFEE64135555A52A79E3F7F3F37BCFCFCF7E2FFFFDF9BC77EFD2C7D6050C424449DFDEDEDCB22D485CEA2132EDDB119BDC7ACFBE77F7CD377A079E7F693DCEC6DD16E3382709FBF63963B35DC709DD73E88370D3476E40433A619B2EF51DE05167676BEB7D677BDB811CC2E65896D57B1811867C18FFC27F1D50E2C2198B003EA21EC4614AE75F4631AA750C7C18CE800BFBF630C4807827930F21C05CC8CD84C3B6F630025C9A11C413DB02845006189775F75108472CA0643A9A7102C067F319E4E3260087305DC36E3EDC74395B3B62394ECE9841B951C8A8DF1270FB76AA1FA7CCBE94966DA93FAEC103AE693617AB8EB5D8B7F702865CCCD75E9E6B77800331AE4EC79B29E78655FABE211D83FB8FF8B7610D22CCA200F6098C5800F086751A8D31723F86F333FA14923E893056C5E482F26F0502279D06740603367F0827A9F043CFB69C229F5366946C0A4FBA2EC26EEFD8D6319F1C8C31945EA0E860C468003F8004068041EF14300603223060ACC7CAECA5B9CE1013AA4DA6E37EC7B5635B47E0F201245376DEB7EFF07D73882EA1971152091E11C4371DE7614104174DC2B70BE3E2344CC37FEC641E1E181E05F8CA273A05F36C8EFB94620888C648CD10A96F86036EB6290DE68B8DDE8C378A7C1F04F386A5F30DD7C1D2CFC034BC72FDA6CA19711F8FF2D978A4DC2C7C11F4D68A1A12C4F6B9D23358F1F3198FEDAD811EA0A7F038F25733DB80620C5DB6320E3F3582D62E740C2ED0340E2325B4A379D93D6DEB21C4F1D0F01CCD8AC690839E6404EE0201F51F52AC717439E8C91908A6504405BA68E4884681DB4A7EA10FADCCE28356CEC2878A6CC5AF3A797A4E7E72999C678A6A973DD824C4EB13AE7E2EF17F43C0DA7ED7285EAD2DC8D4FA74EEB3DDEFC4CC9F17EFC46C5F2CE5F9C9A66CEDED82EDB587D7CFB5E7BA346A4CAF0CB3B8964E7E0AC2F0331A786B9F78047017C964CB598F91FB744130E9245B1EFAD375E4B062A7AF3D2EDE47013BF7F2EC398F8BCD7C47F3532C92EC75DBFC233A5E74433254D422BDD02B37F807116ADAAADD4CD259769D0597847981B72C77681AA682E5C3519F289A1D88915F4D04D5BBCC303CC4FC822575B46C669863767670723FF06080E71C423DD88A863882FE1806E9FAFEFDF6EB7F7E796E5B8F018EF8AF5B15B31546BFF8EBCB17BFFFFAF2A76792617B01433CFAEF67BF49861D1386173FFFF1F2FB1F24CF6D139E975F7DF7E29B1F25CF9DAAB913C3AAC4BD30A42E8AAD5693F8E7BE5714E080789671C656B91A2BF78A236E5734E396E4BE28D4594E754EC83EC490416BCF4D9EFE0620748157DD5F7C815E7B19E51DAA226359B4772A33F2EC0B0622FD0178C0372D774B4458355543C44533804DD5550230CCF68402E454E52FFB70068948D44CD5612283EE71A82A939CBA64AC45BAEB398A6F36BB6C3144D6B9404DBCCCCD9E24FE6B71C79A6BFCBA5C50AB8A35B89D76D926F3660F476B70AFE450148FC19C4346D8F871F89269CE3A2E5A7ADC8569F25136BAC01B415639ECF3E3B76C74C70C22DD78A8094C09B40B50858E7540C9B628312BCAAB954BBDDF2BE3173E06946DDCE6A4918BD26BA9E23F6D4E080D7675D3171563A0B4527E575554437433886F8AD0A9811B74A00F4BABAD3B4B33E58ECACB904E5287CCEA954E4DC1B2770466339ED02905CC94628D92EAE5E0D6A87D49CF4F301C37D454F6A4B4722646033085A5AF223078F01005A1B84F803110E9F0C0F335C392F851B305B3594A21A26AA96C6F660CE2E7343437A6DA9A289B421CF265F92244C7EF3B555B5739E30232C020D0BC260D288E7C527F58D473A737581520259963C8129E8A22896D70B2125D1128A39A23C5353815242698F3EB722C15CE2407AB4797E5381552125BD82E2EB7154C17535AAF33ABA56996987D32C7CC6FF905AF945473245944538124B18D5FE575B4A267E57473B42C295291EA1225115E4B1BBE924A55A24B259F2D46AB56B14C3985BB8B6A7972B374786B80B89A38973CCEAAFC09E52AFDFA9A6C9F241C1D983B4E41DB9B58CF76356695E58D42E8CA886D0E8CAC5C513C35326A8BE01ED71F0A913DA6B47055594B28B8ABA4B670D9B45A50D06F4A3347498A012A46423147C85FF655949C6A8E94BDF5AB3819CD1C257FB95771726A9B95D1F2A2681BFEE4DD5D054828D771DC165FD575FB40F7BE1EE35D65A0ABDC64CA43E4ECF24653BAB9F4D25BC4E27ECCCAB52219625B5C5517C813578AD13C64D0DF140336479FE2014671969B0D3802044D60C892C7717B676B7BA7D4CE79735A2B9D30F4B0597FE5DA2BE348287561EDBB65DDA850AE23172070CF415029D2E6A04BF42F66A86FF9E0F2EDF658C51EC595C0943EC431621DF620C6A6A93CC80D89072FFBF6E731FBAE35FCE4491561C33A09F81ED9B5B6AC2F566D62D49A2F6E636CA726B56771257D6BFB12635DADD81FE4F19F59074D88CBC8526D415C06A5D88068EA3F09570B9F59BECBEED5086F6A038A767B887684F53863BBB6AF5743FDA5562B83F365A94EAAAE70D546A9DAC0B754F35347276BB1D769A5D8ACF63375B533CAED4ABA9D61225BB17D69354B945B920CD6DAB203692533A85D462B01757644EA3A89CC0C5917E1AEA2B5E306B5725C63C7C61A3B345ED5468CEB6ABC58BFF3D4BC5D5EABC3DC80768A72D5B4E641B974FB6FE899481E4878D01C8B23220996B5E5EA051D1573A37E8AA619EB9B0EB47D17B56D17BA39B475FC1BD291D1D481A1795FD5ED4C4D09FA86755954BA2AD6B0B0166D14D54746BE7F953F0DE7A12344D31C42FCA138816E61E7CA314332A159002949940D29A79190019EB900B1C6097019FFECC2308C3BB9D3D6D4037F0CBD213989D82C627CC9D01FE382778840D4347FDC2B5294B977328B3BA7BB5802171389E4EB84DC8F10F6A4DC879AFB650D848870E9954ED89289ABDD742E918E2931044AD52703F319F4679883852764042EE032B271977D00A7C09D676FC5F5208B0D51547B6F1F816900FC30C5C8F9F9AFDC873DFFF2EEFFDCBA397821410000 , N'6.1.3-40302')
