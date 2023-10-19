ALTER TABLE [dbo].[Orders] ALTER COLUMN [PaidDate] [datetime] NULL
ALTER TABLE [dbo].[Orders] ALTER COLUMN [EndDate] [datetime] NULL
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202307090635303_UpdateOrdersTime2', N'IslandOfHealing.Migrations.Configuration',  0x1F8B0800000000000400ED5DCB6FE44819BF23F13F587D02349BCE635682A8B3AB999EC912983C349D59711B55EC4AC75A3F1ADB3DA485382201121217C4110E1CE08010B79590D87F8619B8F12F50E547B9DEAEB2DD7667148D344ACA555F557DF5BDEAF1FDF2BF7F7E33FBFC3E0C9C773049FD383A991CECED4F1C18B9B1E747CB93C93ABBFDE4FB93CF3FFBF6B7662FBDF0DEF9B2AA7784EBA196517A32B9CBB2D5F1749ABA773004E95EE8BB499CC6B7D99E1B8753E0C5D3C3FDFD1F4C0F0EA6109198205A8E337BBD8E323F84F92FE8D7791CB97095AD41701E7B3048CB72F4659153752E4008D31570E1C9E42C0D40E45DDEFE1082000D72AF6831719E053E40A359C0E076E280288A3390A1B11EBF49E1224BE268B958A102105C6F5610D5BB05410ACB391CD7D54DA7B37F88A733AD1B56A4DC759AC5A125C183A3923F53BE792B2E4F08FF10075F224E671B3CEB9C8B27936749E6BB019CC76108A36CE2F05D1ECF83045757B17A8F25F0C4E1AA3D216282A409FF7BE2CCD741B64EE04904D759028227CED5FA26F0DD1FC3CD75FC158C4EA27510D08346C346DF9802547495C42B98649BD7F0B69CCA993771A66CBB29DF9034A3DA94D38BB2A3C38973813A073701243241B16291C509FC0246300119F4AE4096C124C23460CE55A177AE2FC2E3A2432487883F13E71CDCBF82D132BB3B99A0559B38A7FE3DF4AA9272106F221F69216A94256B2819A4BEE3B3C8CF5EA021573DE39FAF91C2591342DA9334734C4FA394176B3217E09DBFCCD7812378BE29494E9CD730C86BA477FEAA50FE4A3CDFB2628A2CC4691287AFE3A01E115FE5ED354896102F57ACAFB788D789CB0D7836AD35CD44FFDA2BDEA3C6A9FBBAF6B3006AF4EDA999BA35A97594E9D51AFDD84B3FC831BF4982AD77740536551FCF6324F9206AABE4E91C2DDB324E365D8DC6621D8620D968A67ED4C7525E8365BA7DF626F1328169DD11365375E158B6FD95FF15BC5887DDD6691E070174B3CE745A391AA587106C7FBF7EA2B2FF4D7EA2F227D68E8DA8916EE4A452D5AF6CEC6225959793D494F939FDF8F1324AC78C3F48C7C97C10C6C67EEDC3EF52AC6DEB800989474FACEE0BFFAFB1AC079FEE56DCDB644AB436A4AD262AEC8846675B497EA194D6D28E9B3D4AB8BAAF67AE1BAFB561A061B46929E457204D7F1627DEE01D2F40D047D06BD9EB85EF7ED5604C7A89EACFC2E510B136D6F4C1EDE2733FC9EEBC3ACAAFEDA2BEDDF9E62AC09B81A1D7FC47F14DD34ECE90514D7C89B7BEE05FAC7D9DAAF6D3496F9B82CAB8148D1BA4A59DD3340C0579E7280F145B39C472D38237402DFC22D5FAD13DAAFB1AF3E850B2D3ED7AC08169C835CC6883DCB5FB928CD1085A1E9C5272FD96AA59EBA7B482A0A7F25A9DF4F532F15A45B079BB471D55F7D5E09BB61346CE031F8DAE9F90AE55D72F43E0EB02BD6DF67D7517470F695F2C8407BED7C60ABE8C5A35C3D1E72882823BBE4A7C1776733C7DF8403C0C34693CA2CEA440ADEEC68E48E34DA8A1493D4A6E7FDF72F56A7F22F92C7813591DFB6355E5B16445BEA8200E2D0F3A5563CA3F76726C0C63ACDD1BD5FAD1C9A9FB1AC5809C428099ADBB5C3AF8B48F638B1EACD4733FC02233DFB8DA1DF796F6FA2D7D935AC7D621A361D535DB597A1A8065FDF4A785BA15A47AD335C4386448820D22412F1FCBAD7318DEC0A49CCD7F7FFBEBFFFCE59B89F32508D6E8D77D81B74CEDF7FFFAE5FBBFFFF5C39FBE260D0E1A1AE4B5FFFDF5DF4883439306EFFFFC8F0FBFFF03697364D2E6C3AF7EF7FE377F246D9E8A8B5B2C235DF82C4D63D7CF178BDD1F0A77786CF728F8700C2FF4EAFB59B2FF39474BE9AFD0E22161C31CE40DE265F402063083CE33B778F03507A90B3C51EED19C3CDB8191FB306160E46D113BBEEF09DD22430D136C29413047BE0F89A31F65A255F723D75F81C08C515C7343B78079403AE2BFBC802B18618B6EC611931150A707E240487FDC2235B16B36A5C4D0483A25B7430D62A0BB2A120481BAD01C52547517CB8D5AD4AF94AAD9359C9CAAD96121A9CCEB995144963D9B558980E2A0B65EF6229C1E441C15EF07861241292B06103BE9B44DFAADB6A4A38897FC8851B5B40DE78DF51233E7F94DCBDC44DF5C847A95E856D2A79DC10052A85DA187E0A26527142A71D11E57D4C2521E569B8BA1EE88837930489D15EC9A206AE63080186A56C6A477EE846D5441CC3D6793A4B0A7523D881E739235A41FEF246CF4A80794329AFB3BE6718B2311FC3E1DB5203BEEFCBDFABD2CD5070DAD3CF548CBD31B5E5230BD05CC54EF6AEB3318C5EE54103D29410D25631265ECECEB88517BA506AA78D164840A8D68684CB945190D265C6920950B9D8C48A9EB0DCD29D32623C2F8148E14256202B3C547D354ED86F7D5BC16989FCE90C9D172236895F9A18A48AF9E0F6FB9586E98734AF63454C9ABA69302CBB30209BF682569E69C668F6FB0162D98C63D0D1219A5D99F1AEC50A94197FAADE1817C63B99D792B9E5E88F337D840596CA1A8C9B0264BC316FDA6693BEC91DE238ACC690CE68DC3796A1A9501D6B04417C05394585BDC1B530A8FA4E686185136C794EDE6CF44914DBAA6997175B543E218F26D362D12C2CB82D95491393E3B07AB15E23595495E96388B228D7CFEC9C23EB73A2C684C5DC6F4F05117E9298B13B084DC57AC411E3CF59314DF7A811B80EFA2E65E28A956446D0A075FF5220FCCC4C5AA3C7FD50EFF5C86C52669DE9248B7A4748A26896BE4F3854A9F2A1270707A3F084022B91B4656661D46EAB05DDD9A449E34094538AA1D05B99664C6424ACD2955F13F4D47B5275053A14E4A68429A0314AC1FDC1A093B10412E840D1B2B6E36C2D8A714B617BFA1E4AE7CAF4E13288B6C64B74C2C6665B72CB4A153250EB384AA52734A7966304D242FB0965AE6624322BEDA8B0F357592244C932485166B976701334B979758F0893C336098454AC7B03B24BF9726440A6DA4A94EF165E5A92EDF96351CD98251DBA5FE6C597D08D1DAA869486CC7BA156FA8E8F645C936E57AA4B52FA2D51E965B16781B2CB1BCD9769695A4303236B92AB47113554A22EB2BAA520B939EE71832F63C2FB11055922FC8882B29B510D9322390E16F59664EA548F8A3691425E614EAEC3D9A4A5D6A4EA9CAE7A3E95465E654EAEC3C9A4E5D6A33B3989F546CD3BEC85FA009142563B85B36734EA607B217EF39BD710C1D73E4D383BDA38FB5EDCD9EB6F576ACDF781B335DC826C66BB6144479AE4BADC33E69CCD7828E3828E6C3CE684579DED5833E143734F69AA068B71D1DE86E41E9742E6679A9725B6A65869648AEFC604BAFCCBA12E9951FC6F117552A15EB2BAA52734A24B98A26440A2D4644B2AD981191523B4A6562024FAA2C1EDA4A736F3CD85DBAF6F9877E05C5D5DB215BC6DC38F460D134172B06764DDB7A57B7AD751E0F4DA52EB593405E1F6C7581CDD4610367FACB83DD960B573F7C15D23BB902E2AE7A66E5B54B3392B0700F535499388845EF7C0FDFC12C366906C33D5C616FF1D3A0F01975857310F9B730CD8A9C9EC9E1FEC1210744BC3BA0C0D334F502C9B5951A19985DB50152017DCCDBC6643F6BE0040683377A0712F70E24220A6FC7B4350FFD9C7587D8CD79D01525C397E23A9F451EBC3F99FC3C6F75EC9CFDE42D69F8C4C983CE6367DFF945EBBC3B05CEEDC721460C1C9154889E3232D40247B6A2FA9D10DC7FD79E168B15DB891885077BE3B796470916ACA56052142C24541C1007262B5DBE23EBE5A3B163BBB19BC3876D63037A33471C2A6C9BB18898B06DA848ECA281E414ADB668CF14572F0FD6B01501B146313022C930C2680768F971B09F039134F02CAD3022FBA24B43402A6D5E2B58C79E7C2A8BE2D8C92CD3488D7D69060FC428D30C93B1B1C08CDD5682075B3498AB25B662A765A031AA3A11EACD45CA3012CD16B21542E1C761E81ED836471E1875898A7900C1B6AACF010AB6198A044CB067F995DC613C58C99519A0AE7E54C4C0EB97320371D72F6906C16EE48851669B5980BAB65AC601D6B525C303D8F516DFF1F8743BBCC3926D78259076A6FD338DBB0D8382C36B36A3AD40DD3E0E0BB80D09E601D21496C47A03D0592B646868DD02EC9EB7C3BDC1608D8F77C5FF81BCA1F1AD06040852E7D1883D8E8D8AD11DB86A8780AA46C4A31A50BC3E5698A9B160A586171EC573F15105E60181458DEFD3185887B6E854BBEECDF48F7C77CF95D9003C31E9E523202F7119D56DA07D5A898FFE5957EF22A47A172B76F4608099C6F454C3CACA505ECA5C4876005E89CFE7E7974A07A26480A154BC4B43FBC61B7CA352EC179B006B149DEA709634DD18D3A7627C554754155D8F6AA41A29569312AA49D6871C908227CBF86D813AF355D6890E51450EF3A4467992D1970373E810A09A00A064BD68414B4683889201CD482C80FABC416CAE422C7B00C84F3AA427497A603B768E8DE62420CA8C38B1ADC135B5156F89A91292EABA4F7B2B304C12FB24640BE8A6CE584E2A7FAABFE9760458B217DEEE53B24050129FCBA300691DE1F3E9E2B71730F597358919A2194197098D489DB3E836AE22346E445515FE210BCC8087E2262CE5B7C0CDD06717A669FEB747CABF0DF132BC81DE5974B9CE56EB0C4D1986370163D670A4A7EB3F878962C73CBB5CE57F09A88F29A061FAF848FF327ABEF6038F8CFB5472A4AF208143C8F2BE06AF6586EF6D961B42E942485253112AD94722DF6B18AE02442CBD8C16E01D6C333624AEAFE012B89B2AEB414DA4792158B6CF5EF8609980302D69D4EDD1AF4886BDF0FEB3FF037B787B71A58A0000 , N'6.1.3-40302')

