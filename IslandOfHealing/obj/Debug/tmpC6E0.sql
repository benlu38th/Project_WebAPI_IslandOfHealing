ALTER TABLE [dbo].[CollectLikes] ADD [LikeDate] [datetime] NOT NULL DEFAULT '1900-01-01T00:00:00.000'
ALTER TABLE [dbo].[CollectLikes] ADD [CollectDate] [datetime] NOT NULL DEFAULT '1900-01-01T00:00:00.000'
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202307080459403_UpdateCollectLikeDate', N'IslandOfHealing.Migrations.Configuration',  0x1F8B0800000000000400ED5DCD8FE43815BF23F13F443901EAEDEA8F418256F5AE7A6AA6771BA63F34D5B3E2367227EEEA6812A7489CA64B88E34A0B121217C4110E1CE08010B79590D87F8619B8F12F60E7C3B113DBB153A94ACF6834D2A8CAB19FEDF77EEFC3CE7BD5FFFBE7B7D3CF1EA2D0B987491AC4E8D8DDDFDD731D88BCD80FD0E2D8CDF0ED273F723FFBF4BBDF993EF7A307E7CBAADF21ED4746A2F4D8BDC378793499A4DE1D8C40BA1B055E12A7F12DDEF5E26802FC7872B0B7F7E3C9FEFE0412122EA1E538D39719C24104F32FE4EB2C461E5CE20C84E7B10FC3B46C274FE63955E70244305D020F1EBB676908907F79FB05042159E46E31C2754EC20090D5CC6178EB3A00A118034CD67AF42A85739CC468315F9206105EAF9690F4BB05610ACB3D1CD5DD4DB7B37740B733A90756A4BC2CC571644970FFB0E4CFA439BC17975DC63FC2C1E784D37845779D73F1D83D4970E0857016471144D8759A531ECDC2847657B17A5724B0E334BAED30981034D17F3BCE2C0B7196C06304339C8070C7B9CA6EC2C0FB295C5DC76F203A465918F28B26CB26CF8406D27495C44B98E0D54B785B6EE5CC779D89386ED21CC8867163CAED217C78E03A17647270134286098E15731C27F07388600230F4AF00C630419406CCB9DA9ABD3117E3713121C121E18FEB9C838717102DF0DDB14BA4E63AA7C103F4AB967211AF5040B4900CC24906258BD44F7C8602FC8C2CB99A997EBE260A674D88684FD2CD313D8D122FD6642EC07DB0C8E5D02078BE2A49BACE4B18E63DD2BB6059287F05CFD7224C8985384DE2E8651CD62B6A76797D0D9205A4E28AF5FDE67196788D054F27B5A699E85F7FC5FBA871EAB9AE031C428DBE3D3153B72EB54658AFD6E4E320F310C7FC2A09373ED1155855733C8D09F201EAABE4E98C886D1127AB758DC63C8B2290AC345B3F1C4294D760916E9EBD49BC48605A4F44CD54DD38966D7F11BC811759B49E9C667118420FAF4DA797A3517A8896ED1FD64F54F6BFCB4F54FEC4DAB13135D2AD9C75AAE695ADBDDD49E5E5243D657E4EBF7E2A46E99AE903E93A8507ADB5894F87F0BB1C6BFB3A6046E2A32756CF45FFD758D6FD1F3EAEB8B7CB94686D485F4D54D8118DCEF6427EA194D668A7C33E225C3DD789E7C599360C348C362D417E05D2F41771E26F7DE2390887087A2D67BD08BC371DC66490A8FE2C5A6C23D6A69ABE75BBF83448F09D5F47F9B55DD48F3B5F5D85F430B06D99FF24BEE93AC91932AA8B2FF1C605FE7916E854759849063B1454C6A518DC81967E4ED330146C3A4779A0D8CB219687167A00EAE117B9D11FDDA37AAE31AF0E2527DD752F38288D4134AC84CFBAEB29C90C1CEDEAAF5639E4BFE67AD61A2CEDD0D264792F3B8DCE224E9FEBBB94B3F434048BFAFD8EB57257A406D36C62C77D98842B428287B0288F7318DDC0A4DCCD7F7FFBEBFFFCE55BD7F9128419F9BAD7929ED0FBEDBFBE7AFBF7BFBEFBD3376CC07EC780BCF7BFBFF91B1B706032E0ED9FFFF1EEF77F60630E4DC6BCFBFA776F7FF34736E6495BB88518F9C693348DBD20179668045A1735E2F4CF91EF18DEDAD497700CC2E74494C192088F808D72B0697E2FD13318420C9D13AF78AB3703A907FCB666913DF9B60B63971EAD85B11748E2FA7ED09A96B8059850BB0CC219515902C700E1B60F0990172C4168C6A8C67043274479C0266A3E7906971051FF61C6119315702EA2BD10365F43485DEC9A4E38181AA1537205D00103DD7D400B08DCADD536A1AABB3DECD4A26151AA66D7F670AA66870552855724A340560CC055105044E3B5D88B6BA5ADC0517149BC2D084A59B105D849B76D326F157B8F022F7994A8126D47C8588B5838B47589B98BBE39840645742FF46977B005146A25F4F85C747162A0EFE8C9081690E6EFEC1F64E94E4457CA43415ADEB534A144E9CD2156BD5BAC8F288AE0AD854D29410D256312A56B0974C4B850A2832AB522324285E1EF18CCA1464643D0E606294EB0AD2DB65FD772BD3BDEEC36B1677E64609BE3A5D5C2B279A4DFA657EFA76944446E98734AF6524AC9ABAEF0D5328095F08B876637E73481A7812C7A30AD7129D96694266832089BB845975AA5E1813CDAD9CCBE15573AEDFD1B78750BBFCE6D4634141AB6E83DF97AECA92E9A98DB60CFA6932207B96C984E14C9CAD373B05C0668C1252F972DCEBCC85C9E7D32B74FE78D0A1A132F9564F5B2D5B299709C80056C3CA5ACF3E16990A4F4BA10DC007A3336F32349B7C2492A2C7B358BDC0FB6055799FC6A1CFD5C46242699C592D8A2A4744A36497BE4FB854A63DA26E0D08C72108244722F4EE09545481D31A9473347CF9350787FED2AD84B1C612DACD59C5215FFF37454670235152E6EE30969C239AA1F0D19B502BE162E5A41B308371B300E89C2FEF0DB16EECA57A43C81B2C906BB652EAB88DDB2D1864E95AB2A12AA5ACD29E5C9A83C91BCC11AB5C2358B04BEDA6B1835759697CA93648D16B2CB134F05D1E52D167C622F3D0466B1D631EC0E4B29E509B1461B34D559A5229EEAF64D59C3912D1817270F67CBEA335F6FA3A621B119EB56E4FCF0E38B964DE27A24D917478201C49D9FCCED452C1FB619B1B2AC39C126578D366EA2CA82137D45D56A61D2F3B436C19EE72D165065296A025C59AB0564CB243481BF659B399522C78CA751B49853A813C6782A75AB39A52A858CA753B59953A913C2783A75ABCDCEE2E6A6629BF1453A174FA06819C3DD8AC95A323D90A56DE5F4C63174C2597F007BC7DF22DA9B3DEDE8CD58BFF10E66BA90AD1DAFD95268E3B96EB50EFBA4315F0F3AED45090FB6AC15AD2BA66617363BBB6A6A5C294DCBEB9DEE22F9D67D4FD1C57508A3EE039FDEF5CC572986D12EEDB03BFF79380B83FCC45775380728B885292E3299DC83BDFD83468DFDE3A9779FA4A91F4AAEC7D445EFA2D4B6906E1950DE7626545AA7000AE5E5E81E24DE1D48DA05E66B56D1F8E4335EBF7A3CE7C1BA09A081F4270BCE900F1F8EDD5FE6A38E9CB39FBD6603779CCB84E0F9C8D9737ED531BD7509F787012321D35E0AA22702867A94485754BF178187EFDBD312CBA0D722C6953ADF04BDF1282973B6042647C102A1ED0535EAA4A5E23BB4161F5F16BD1EBB1BA5CF7D6CC060E6A851F0DC672DED72E73E542476D10039C5A80DDA33C515CF7B6BD88AC3B74631680DD176C06857ABF961B0BF511F69E0597A953F0E4597AF6E54DABC5E158B03F954B140712DB3CC17210EA519CD1A43996698AC4DAC395C4F12CD3A4283BD5A960DAE2506BE34702D4283B94859F99F99207B15DF7D1886EE3D3BE6C803A375A2E2666D5C6FF8356AE3FAAC455217B7A68F1EAC2269FCD2A3E60FD26DBBD4688BB51AEA24A2B11394375143F4886A86462C0DDA22BC3ED48A9FB12A7CB60F1EC5BBF25101F31ED5ED8CEFD3841282BE85428FDD9BE9DF708EEDCA54B536CD6CE3A608751535060535C5DB2C12D8DDD0735811D075D5512826D515DD68A631A6CF396183821CDD8CEA020A69E18EB26E473687B4264157D2D355D1239B4497E83F5ECD8FAC72C0A442A5815659FAF97B58CAA32BDD91A4FDF463E7D8E539AD729C1137B6B1FA9BBEF09628792B5966237535EDE406E298B8BF13405C631A2C6A12F4AF0620E8092E89F53943B771E51C1B2BAABA34AF1D21063EF1579447B7C0C3E4B107D334FFB9AEF2F74B9E4737D03F4397195E66986C194637A1A014D4C3EAE6CF8B87C4354F2F97F98FEC0CB105B2CC80DE755CA2A75910FA6CDDA792BB0E0509EABACB9B312A4B4C6FC8162B46E9224686844AF6B188E31A46CB90104B2FD11CDCC33E6B239AFA022E80B7AA7254D444BA0521B27DFA2C008B04446949A31E4FBE120CFBD1C3A7FF07EEC39F0B2E630000 , N'6.1.3-40302')
