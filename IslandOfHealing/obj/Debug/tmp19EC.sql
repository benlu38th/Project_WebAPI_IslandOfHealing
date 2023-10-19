ALTER TABLE [dbo].[Orders] ADD [MerchantOrderNo] [nvarchar](40) NOT NULL DEFAULT ''
DECLARE @var0 nvarchar(128)
SELECT @var0 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.Orders')
AND col_name(parent_object_id, parent_column_id) = 'Guid';
IF @var0 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [' + @var0 + ']')
ALTER TABLE [dbo].[Orders] DROP COLUMN [Guid]
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202307170330111_UpdateOrderGuidToMerchantOrderNo', N'IslandOfHealing.Migrations.Configuration',  0x1F8B0800000000000400ED1D4D6F2439F58EC47F28F509D06C3A99EC4A304A763593994060328926D95D6EA34AB7D3296D7D3455D5B369218E487C088903880B121C38F0218480032B21B17F8699DD1B7F01BB3E5CFE2EDBE5AAEA8EA291466997FDFCFCFCFCFCFCFCFCDEFFFEFDF9C107B751E8BD06691624F1E1646F6777E2817896CC8378713859E5D7EF7C73F2C1FB5FFDCAC1B37974EB7D54D7DB47F560CB383B9CDCE4F9F2D1749ACD6E40E4673B51304B932CB9CE77664934F5E7C9F4E1EEEEB7A67B7B5300414C202CCF3B78B98AF32002C50FF8F32889676099AFFCF034998330ABCAE1978B02AAF7C28F40B6F467E0707292857E3C3FBBFE0EF04388E44ED962E23D0E031F627301C2EB89E7C77192FB39C4F5D18719B8C8D3245E5C2C61811F5EAE9700D6BBF6C30C546378D454D71DCEEE43349C69D3B006355B6579121902DCDBAFE833659B5B517982E90729F80C523A5FA35117543C9C3C4EF3601682A3248A409C4F3CB6CB4747618AAACB48BD430378E031D51E603681DC84FE3DF08E5661BE4AC1610C5679EA870FBCF3D55518CCBE07D697C927203E8C576148220DD186DFA80258749E264B90E6EB97E0BA1ACAC97CE24DE97653B6216E46B4A98617E7FB0F27DE0BD8B97F1502CC1304292EF22405DF063148FD1CCCCFFD3C07698C608082AA5CEF4C5F98C66587900F217D26DEA97FFB1CC48BFCE67002676DE21D07B7605E9754487C18077015C24679BA020224D51D9FC441FE14A25CF78CFEBE840BCE18D073D83073030A2EC4B49DF86A1815EB198379E1BF0E16C59432004FD715C889F71284458DEC26589672A4E6F45734C74361739C26D1CB246C3062ABBCBAF4D30540339FA8EB5D24AB74C6207C306D16ADCE52B65FC3F78B57DED765908740B174DFD55BB96D1222CED51202FEE9A41FB8C77F9886BD7774EEAFEB3E9E2490F3FDD87691674770DA1649BAEE2A342E5651E4A76BC5D0F75D4CE5A5BFC8FA276F9A2C5290351D2131D5148EB54DB890ED7028600617AA31FB48853B27B6DD8AF85A74B789F87A2BD045FB280911259E079F0031CE448557C406D6602CACC06D49E25AA20D496B07C5EB5545675CA9A69288D27C25D9762AA8698E3F625E21CEE883104FEA03871BFDD5149F733F98D71C04B7D94FB293F83A11A2C7D584B348949108B754E586D056BF93D642F09D85E642B4BED75EFA95C8B6DA367B862826BA9B4E806038D9A92AF6E98A4F05C60E25CBD388A5D867B729F5E6D0E51C42EC00B607120CE27E6DCBFB42FF2B34CDBDF736CBA4D0A69F2915335B8541A29C29540B2BCE2F7507636E47CDEE395CDED7E3D92C59298FC59AA76F43263FF7B3ECD3249D0FDEF1851FBA300218F6FA02AA762DC2C48995E3245A0C617B402B7D70B9F82448F39B7963F568E4A2BA1D54A843A4780C3DE7DF4DAEDA2C5B9A846AA34BD2FB847F7B15A896AA9B4E9C19496AE1523636E3968FD3004A50A1F587FD6488D54B28A43F3D05D115DC7BE0EEEBDCFA22DEDD358FD6EC2E2E3E78EB62F4EC7609E24C8251F5F1556D0F6850A2BF706765E6B3E979FF18AAE3C9A7E5248A31A36A08F0137DE7B01456EA748CAFC66DA1FD542DEF1520795FE85E20F567F9C9F0AA880BCBC1B9BF7622321F47A41A688B4B606E53EEFFF0A3B03B6A8B22563C4A2495D5F266E492F122A7DADF2F75795F25A1BAAFB992D41FDF244F400572DE15E4B06C6FB5CFB14B40B9195A2D04CE166EB1183818F70B42DE574121177B10A6F946AE03EAAAA6DFEB1D7691E85E07753106579E0DF0E8DFC5C5AC8671BF5E147DB9B2AE38BA1E1A612D69387135BC24BE3C515493199385753BAD9DB3746E654F2EDADDAF11795FA7209DDDF8715E10EA85CA32D5CFA10A8A5988A81B5BAB55D7CF223F50C9883EFB3EBF49E26DBAB01228124E003D8BDDC04186E3515809757C9E0633D04D9D7262618068C041238C5C288AEECC9E50936A50136E48850C7AC5D46B3622C1676E0312D531B5862A4E623578F60446964B717270E22209637ED66A5ADFEF88F2BE461120C7C047C456F9C9EEBDE7E2C6D181947A128488658ED633E565594FCAB5E5EE255F63AB885A61F56DD149761CFA8BE64194C5722B41395B6B90705090846B08829C3E9A5AE58555359A2F7FF1D32FFEF8F9C4FBC80F57F0E72E475BAAF69BFFFCF8CDDFFEF4F6F79FE1067B2D0D8ADAFFFDECAFB8017FC8113478F387BFBFFDF56F709B7D9D366F7FF2CB373FFB1D6EF32E3FB9E5342AA696BD0EEC3CC134C011A7F9ED6FFFFCC5AFFEF9E55F7EAE3DD3FFFA07D3A065A6CBDACCC4B54C76D98699B87D9D897B9C65C92C2826813E5173CEE874F7508DF4343DD39B5B627CE23D8593132CE17440298108C2EE6467F15310821C788F67E5FBC5233F9BF9735E60C131CD4D11C3B7921C62F8A91C8DDF37B86EE10E0B52B4C5F9213C7F6790C18238E7B7E3209E054B3FD42314D35C733F4734C01DB15F9E822588D156AC47111D0C087B0B8F08EE8F99A436721D4C09365473A7D8FF54C6022DCEA80D0350EEE06DB3DF065FCE60BD72BE15532A4730004B2A67681B1852EE22DA229754FEA21CE3105ECD43CA4ED5239856E6762B36E5E41A4E70CAC961C0A9D493C2515896F67B92B180C409AA99F6F2603E083B4ADE3A0DC58242520CC076C261EBF45B1BB746612FC6734436A73237926652B17397FE862C73931B906BAD384C8CF7002C269E858DE731A19B868C29D43E1B0D6B30DE46FA5CA7767EDC74DE53613F0007AA6647A77B89CBD1286CD9E62E2163206DDF09E2BD3DEF11A4CFB0DA4F71B7E004A339960138597316753061FC86C63CD9087D165ACE0D6A070681DDA571D3D167621D07887606DEDDD9E10D725D0E2A2A7C863BAAA866601B8ED5A2FB491923282F2B1B06A8FC5AF4194C75C149453E216E0A374D3A2AC63000332A66464B0AD2F7EBA332A252C314DF493B60BDEDD22445580FC8651B7C82292F449150862DF0B54D21A46F45E10F216AD5955856DDDDB29C82E05D809CDDABEBC841CD0DACE48A83633D214005A456107408210E0C6572D7C4A6329D052ABC0853690B5434FF2240E5E26A69DCBCE3E3DA63BB450B88F2B892E3032707883990B680139C083888823A7AA46FF4092573357A5C0BD862CD8A4055A2B26DACCDCE201C26B92533A08815CA8D928FC045D46E09D6C50A11FD1B523C3872D9714249FF629387D78C8715FC34353428250900C3D349E3A6CED3BFAB23C644CB15059DD4B7731A54B7208F2262889495DAEE8E346F6614EC44CACD76C652DCFAF44334E621364F28C58D85C69D05817425F21534105F35F4336EF68D253F70952D5DC79A4EE0DD6C588AD14BECE76D14B418BBF8A11D4F81764BAFBEAD971806BBF72A68A2B4EEF64099D637573C918CEC8E569647629CAD3A8495A9B157712C7E772315C8ED262F63A397684F2634AA76A1AC3473F5433CA18F384FB456538DB6B1861846AD1F2A08A332CF90EC4AA98ACE88229357727B41BBC5C06EFCAEE451EDB68B4FA9F8DBC1B44C8150151C4C25B9120E4EFDE512D29AC89D5095781765E284A3772ECCB30944258CE98CDAFAD93335EE294F527F0198AF481B9C83E3202D02C9FB573E3A9F1DCD2341B5F24C2E397FD4BD888FDDFC64D50793BA1DFABB327AE8243610D8312A48C77090A846315E2055F979001E4A68E1877E2AF0FB871AF32A8AE54619796B6C572041488C0D4A2CB0CB39850B2ED58744660D206191E5FAD06A5B110949663F924321ACEA242085B11DAD3666C6396B15C7659C718F665E13D676C9D3F6CC3C14175741C948005591C94AA8A2E9D32BA12A34815347CBA701D5A5FA908A70F82490A2C0986B29C73501FB2A1DDBE4D071647C12242E3498BB22F43D3575BE48B552D009BF57A088854BC790626EE44E13D69E22322EDD18A943D9761C481ED2C86C2E7D94ADFB9140E3ED33D27DB430D0533BA8C064DF0641B00FE35213995805CEA6456255680C87478AFAB031AB4260C673B72B37F725D6DBB302443FAB04FD4FB72F4BFA94D023CD7D798A7330DDA203A9C6148B9BF533AD38E23225B4EA421385A78EA04C6B3D75A9C1C6598444A636CDA2C480557178638A5D71A901CB56217628FA5665FA50CAF8C4248CB2441F42136C9884D294EA43AAC30F9370EA327D284D3061124E536A32B2841D5462D2BE0C054C02284BC6501CE940BFA275200AF9AB82C83EEA2561B2DF0CF8910DFD4BB126FB7163C432BE4D7120996B7F0173E12C6DD99791A70908CB9E6EEBF2A1555D1CE59539E09AAE973ACA2BB5F544E63B0F2B01CA923BA8983037680ED601EDEE62BE1A5ADAF7B326C8C8A92414B2DC54C6728F197851DBFADEE10E70187FD1E880C95A816A309A068C9E047013959492BF4DB199A892D8095A5E1FDC01D612DCC1BABCBBC16E701DAE6FE430FAE12D37C70B9796A7ADE1A6EAB2D6010795DE8FE65C2369D70FA770712CA9F31BFBD148BA813A3E2523DE70B929B42AE4240FAEFA600AAF0A23C9C3AB3E8C73D4AB6343B242DC14120E0E4902C2850618E1E0901446B8D40C5215478D0555150F7EE0A01FA5D05745CAF72AEA19D43B368CA58A914E342EB430B9AF908EFEA56ABDA916E726EC20754CC0A5661CC8AE07D3B5400716A46D5EE497ADDDAD396F26B60AEEBD2AC1BFB13753E54944B93815A3470E4BC5A8B3CAAB89752D2AAB4C3C48A2D7C11CB9155DACB31C443BA8C2CEC50FC272CF682A9CFA71700DB2BC8C4D3779B8BBF770E23D0E033F2BDDCA2AA7A947EC43212D2FAABD7DE44505E6D1946D6EEE8B85A064D99C8A46AA08BF2FF0411A2272698068DB1A9BD438DD6EE5CD54F611BFF691A69352813821993A47D99CC3BF739B90CCA4675347507474E4829C5D03F31740B8876E27F11CDC1E4E7E58B47AE49D7CFF156EF8C02B74C847DEAEF723EB88A362C6BC231C7949E65014F2E3BB143BEA04ADC59E4A34D4AF45FEEDD7CD61D5CE4A0E809D37E92DAF026B7EA49C94AC18938060C0A13C42D8C549317DFBC6D357FA3CB9203793E6D1460638936C0271A4316165AB6E93845DA474394F5B14499D8BB6561C6DD9A621D8400BC7267B19D3B83475E477ECD2648F0BE5C964848EE9662AF1FED95A362E0F760AA98C52850C2309CD52C0DF0DF23369D735D41AABACEAAEE09249D3A51BAE552274470A1D9D99AB934E40E63677B532D8D4E5A295A1831B9DCABCDB4CB0E9C935C66A988DBCD3349019C73B0172A69F89B28ADB4EA438CBB88D3621C92AEE508713FADF6CADE4E5F33DBB1292A3E9EF4C0E686B06A77340DB302399E0C9469FEB5B835038CF6C2D43F3598D6D664E99D358979785403AB176DF0CD1E2E4B2B54CC165F6B55DCDD68755AAF146F3408B33CAD6F280A64E3CB2CDA24715CE3019ECDD987549FE55574A0E9F5ED52D642A7BAA5BD05472D4916D1E6261EB041093FBD41E2126F7A9331B059BDAD4666B1A4FD7166543D5DE13C9C61D557E1345DB2A1FE8DD10877D70309B5B53224B8C8D589D5785289166B77DDFF1CEEB2C11DFF819F7184789C133EC0D1A665D16A687EF71EC90EA66A9F3C6E7262A4030818551AEBE4DE72375808ACD6322CD90A58AA0A21B94DE6EC42C7603CAA8BB9A9C6EAC6474C3338F2474C3A80CB30D29E6C6E4121C049EE85E3BA1DD467389FCB9FA8671895192B831798589F34F206198926EA3F9A6ED7937DFE15D48ED36BE1E2D88D74CE06291476ED3756A9D27DE7CA75B9E72CD3AD79922D274D75C6D9BCE283AEFB537EF0866921A8D0ADD4DAEFA81729631D1AA6D9262D9258554BE2F74CE48B267DB0229B32D29CDC6D48786E595A1F41F7D26D980C4646CAC7476AA2449A22AEB6E63E296987FCB07928793F915BA862D8DE56DB98A249DAA329429BA69874FD919B93EA8AFA27E54698ADA129CADB5D29BA94627CFF8234C8326CD8226EA439C5880058B0FBB1C64FC45045C96A646923D4D3B799AA8AF41F2AB893A7690864D2F0B9B729D49F38F88B3B5C993B5897A1127B05025726BCBE32624A42AB9C76899DE44095904D25C7E71C63797E56DDCA0046EB68317C8592EFA76F761CB2F2E7A4ACCA64AC42688926A47C8B193AD710967461C98C36C6AE64363763B2A3A67F7A1F5942CCD7C98C24D57107CB1FB9007CD82662BBAA42A815EA2B40D4F6866BF8DB14A8A24B25D7772F492A24CA0937061875464A0B425223C9BBBE1764C3E66BEEEBB0FC920BB181F77071E70573172AE2A7F3D0559B068401C4098319851475B5CE724BE4EEA133683515D857313CEFD393CF7227EBDF66739FC3C035906677EE27DE4872B24EDA32B303F89CF56F97295C32183E82AA4F674745257F55FA450A3713E385BA25F998B21403403E48F76163F5905E11CE37D2CF0479380402680CAD910CD658E9C0E176B0CE90517ED4E06A8221FB65C5C826819A2483567F185FF1AD8E006D9F53958F8B3751D3E490EA47D2268B21F3C0DFC45EA475905A3690F7F421E9E47B7EFFF1FE2BAC80CB3D00000 , N'6.1.3-40302')
