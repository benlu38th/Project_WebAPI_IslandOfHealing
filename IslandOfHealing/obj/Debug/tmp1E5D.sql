CREATE TABLE [dbo].[CollectWriters] (
    [Id] [int] NOT NULL IDENTITY,
    [FollowerId] [int] NOT NULL,
    [WriterWhoBeFollowedId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.CollectWriters] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_WriterWhoBeFollowedId] ON [dbo].[CollectWriters]([WriterWhoBeFollowedId])
ALTER TABLE [dbo].[CollectWriters] ADD CONSTRAINT [FK_dbo.CollectWriters_dbo.Users_WriterWhoBeFollowedId] FOREIGN KEY ([WriterWhoBeFollowedId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202307120309342_CreateCollectWriters', N'IslandOfHealing.Migrations.Configuration',  0x1F8B0800000000000400ED5D5B6FE448157E47E23F58FD0468369DCBAC04A364573399C912984CA2E9CC2E6F51C5AE74ACF1A5D776CFA6857844E222241E40BC20C1030F5C8410F0C04A48EC9F6166F78DBF40952FE5BABBCA76DBE9281A69942E577D5575CEA973EAE2FAFCBF7F7FB1FFE14D18386F6092FA717430D9D9DA9E38307263CF8FE607936576F5DEB7271F7EF0F5AFED3FF3C21BE7E32ADF1ECE874A46E9C1E43ACB168FA6D3D4BD862148B742DF4DE234BECAB6DC389C022F9EEE6E6F7F67BAB33385086282B01C67FFE532CAFC10E63FD0CFC33872E1225B82E024F6609096E9E8C92C47755E8010A60BE0C283C9711A80C83BBDFA2E04016AE4565162E23C0E7C805A3383C1D5C40151146720436D7DF42A85B32C89A3F96C81124070BE5A4094EF0A04292CFBF0A8CE6EDA9DED5DDC9D695DB082729769168796803B7BA57CA67CF156529E10F921093E4392CE56B8D7B9140F268F93CC7703781887218CB289C357F9E83048707695A8B75880070E97ED013113644DF8DF03E7701964CB041E447099252078E09C2D2F03DFFD3E5C9DC7AF6174102D83806E346A367AC624A0A4B3245EC0245BBD845765578EBD893365CB4DF982A41855A6EC5E94EDED4E9C17A8727019406213942866599CC08F6004139041EF0C64194C228C0173A90AB5737511191715223B44F2993827E0E6398CE6D9F5C104696DE21CF937D0AB52CA46BC8A7C340A51A12C59424923F5151F477EF61435B9AA19FF7D8E069C35D0735430ED070A0DC4A459F87A8CD2F4AC615E8037FE3C57290778B22A2127CE4B18E439D26B7F51F891CAD22F588B47CEE62889C3977150B788CF72710E9239C49A8FF5F966F13271B906EF4FEB416B3294DB8FE1FBC1ABAEEBDCCF02A819BA0FCD466E93878832BD87407FF6520F8AF1AF9260ED159D815555C79318593E88DA0EF2F410A96D1E27ABAE4E63B60C4390AC345DDFEB4395E7609EAE5FBC493C4F605A5784DD549D385A98F05FC317CBB09B9E0EE320806ED619A78F4083E48A9A023D6B5B56461A2186F41B6FAA38D2146FAAB8641D20C970D4B59C64AAEA95B55DCCA48A96929CB278A96F3F3607699BF103693B990742DBD8A77DC46F4AB46D033981B88FE8EABAF0FF1A0FBDF3FEED9A8A37B912AD0F693B12157E4433665B597E3128ADAD1D17BBB770755D8F5D375E6AA79386B3564B233F0369FA599C7883573C03411F9367CB5A5FF8EEEB0667D2CBEAE0389C0F3167C7237D70BFF8C44FB26BAF5E2DD47E515FEE647516E089D8D03AFF5E7CD9B422341454935CE2B52BFCA3A5AF1BAAFD54D2DBE2A2722E45613B6BF924F1910795AE9AF847EB09C786934C3EECCAA7A0AD426DB9ACC24BB41611972A7D1F78D5758DB9B929598B77DD82C118F2B16BB484EF5A7D0963D482965BBB945D5F5039EBF129CD208C5379AE3EC66BE19ED2F643B604B81FB5EABA8E90A4E2CFBA8FDC42D49F5CC74F6009E9F57944A1DCC160357D51E5148C98CDA032622E5727233E4DBC560BBCBCDCBDC9AAEB6A98BAAD67957518F8A875FDAC785A55FD2C04BE6E1DB4CEBACFAEE36893B68D84D9B3EFB509E5CFA256C5F0E26C1443C1159F25BE0BBBB9F23E2672B819A8D3B8459DA180DFE319045A3BD74D934694DCFF5E70F9EA7822792C4413591EFB530765CCABE0F95047A72BDBD43DB03182B10E6F54E9FB20A7AE6B140772040116B6EE0C77E7FD3E76F57AF0524FFC009BCCE1CAD56E48AD692BAC656C528FB165C88CB06A47E6383D0AC0BC7E59AFC5702BA07A1B6B4870C891042B0441AB8F95D6090C2F6152F6E6AB5FFCF4CB3F7E31713E06C112FDDC1664CBE47EFB9F1FBFFDDB9FDEFDFE735260A7A1409EFBBF9FFF9514D83529F0F60F7F7FF7EBDF90327B2665DEFDE4976F7FF63B52E6A1A8DC428D1AD5F25B6E9D15CC028EA8E677BFFDF397BFFAE7577FF9B9B1A6FFF50FAE4083A68BDC9CE21A945D94E114B767A2B8C7691ABB7EAE0476774A783781AD1ECD1A1DC31715EA9D58B2FB728294E32F903A9097C002E123D969F414063083CE63B778B7F610A42EF0448785FAE4D9368C9CF30B0D23AF71B2EDFB96502D8AB030C1210E048768D2820CCC8F32311CFB91EB2F40602628AEB8613CC7322015F14F9EC2058C7028369388490BA8BD4BB121A43E4E494DE2DA9F526668649D9253EF0633D01D810B8640BDA831A4A9EA5E98691C45FD5AA95A5CC3D9A95A1C1696CABC5D388AC9B227432A13501C13D56A2FD6418398A3E2BDA8A14C502A8A01CC4EDA6D937AABBD8451CC4B7EC0A1526DC36947AD62E634B149CD4DF8E626D4AB45B7B23E6D0F06B042AD86362144CB0F2B1A0C467572211824392EB3B649C5B1C7902EB68B41CA9B3F9C41CA356452BFE2D46C14E394ED7BAAEC46BB095A5B4D7904666E8FBA8D53E66D7F6A07F2B619A5A60F0398A4463326B573FBF6A31AA2D639CAF7BA7B30BDCDF280B2560F6865B6FE6EC0E960B1D18A2F97A112643B28BF6C7623BBF28B9A566EB5A5E59E306F29186F0633D565967A6757B17522989E145083640C512EEC7C1D18B5906F40C54A93011523A2A13035679361307369332832CD5181910C0D78B911CB704ADFD1509C729532102646715094C90ACA136F3E51B91B2E49F1A3CA7C2B92748EB64361949AEF208A78757F784FC84AC35C52B2FB1D4A59356D8B596E8C49E4450FBA66C96936B40C74D14268DC5BB8A2A0349B3106DB3154A34B7FA191817C17653DFD56BCE528F6DF60B7C062BF80EA0CEB023562D1EF10AC553CFCFB734AF1E8D6AE16AB57513C0AAF6DB15C6D32C116D291BE0B22CAA671E964BC78A2FA5085278D3C74CB250A898D54BD09456529EAF97BF30CBE5DFFFB3283EA789ECC1AC9B3FD6941C35326EC4F157C3DFB2760B140B2A6F87BCA14675690F71CBE37B367B4090B8CA9CB38667E8E4B6ACAE204CC21F7140F1F0F1EF9494E66022E013E6E3EF44249B6628EAC98FE54B5C8A7C1A2B2AA7951550EFF5D2E424CC87524EB8A12E9087512E7C8FB0B95330E11C0C1A44A200089E4FD1EE4629661A45E24A94B93793E0DA198FC6B5B415E2D61DA4252CD9168E61A1A8B4E3747ABD66E34926A3DA746A1B6606920CDCE2C1E6D9CC685D5A36065C2629B355E1BD3EED3A6DB1BF350565C5EF0A301CA249B915032BAB023A14CB4C1A9185B58A02AD51C29A764A141F2046BAB654E4C25E6AB3D5155A31376161A92245AE82EA75F615497A758C889BC97C4088BA48EE2C52A6215C685558936D65473ABB0F654A70FED0D6B8E1546F524F5D6F9426A91DB9F57ACB7A25ABB470DC47AFC64F17E2E5DBE4859E7081949F7C52CBA0775CB1604062A96175B8F5A097B04E3DDAB449B8053B141B051A74AB5701139BD03E31EF2140B5325540D8CB992540B932DC91818F99669E62805D7028D51A49823D4C409344A9D6A8E545129D038559A394A4D8C40E3D4A9363D8BF94EC536E58BBB713440913246E066490B64E340769B4A87C8BF3C4D63F2CF6E8DFB64B6FF7AF0A2F49189BD33D5965E8F4F1D6FE1A89B528AF3495B04C9C29AA45A4F4BA573D2163862A39807B76D5454BBBEFD8D0B05A2F9D05002AC6774D017F069143ADDD6430A2F14898EB2F19DA3D12CA3DCF7EDC1228A735C7B4350945B8FFEBB476CFA6A3A33F0A9745BB4F2B6B908573EB0C52B6F908B78E58371E627D5B570766E52A59A23918BE2341049B46811B939CEB488A4DA2195972C79A83279E8F8CDBD59C6EE2F695F3AD36B50D4DE2DF265CCC95B0F1E4D73C068E0D7B4A56FEB36497D2799898E24D5CE02F9F1603B16D85BC7EC428D7EB2B1DB40C211289F85D44E8E42B923CFFDF2F8B1F93B26C279649165E22011BDF13D7C16395BA5190CB77086ADD9A7411133EA0C2720F2AF609A15175727BBDB3BBBDC67506ECF2749A669EA0592E35B910C597A703904AD818F65DB485C60CD64C67C01247A0312F71A24E237403A5EC1F7D0DF593F1FF8680DC552A7E4E2ECCA80E74B3F50731C79F0E660F2C3BCD423E7F80717A4E003279FBF3E72B69D1FB5A623507C65E36E582443622AB5C7878C39B6F88A4585FA8D10DC7CD31E8BFD52452730EA6B14977E6B7B947C89C2D23029040B0B151BC47DCA42AABE3D6BF5D15FAEE8266E8E67B58D0FE8CFB3B1DFA468D316F18B146D50247ED1C0728A52DDAC85FB8845F31068FFE582BBE11C8BF9B9667061B2B7610CDA8E4AFF6E889FA3AF37884EADD8E9FBC2A5C9E7957EB315A17C4F7199E58FEFE4DA698EF8BE46064F012F1B19266D6329E1BB6982A77937E8AB25AB7B2735D0F49F9D807A0BB33276F6B68A94B3B537C7DC565CE877C3656ED8A24B3E4DEB3247E7A9CADBDA1E475DDEA62912DA72B3D6B4E506BF1B262CB270B731632D07B7A9494B41D6B1A7203965DB58FDC96252D7A995C838DD2F324328DD2F34C3173DF2224216AE593AE8B6EE92A3876E0BC3D345F736E5E7D9A0376AE12E259036AD9F29DCAD1914F9748F7B076A8E954DF580EBB0609E8E58E149ACD7849D47858C7BB8DB9AABE71D92DEB84BC72729E53F203F3429E980AC8EEA1B8F628D63539975671BBD45ECA22392880E685E77951B742C2ED0E18D477181665483D92086CFF1631A4377A4A241DAF468A6BFA070FB42991D2BE7981E87A74AD1705F6DB4FF697C955FAC71539934196619157DD820C655B29E893C686B3125FD9BACBD5B94EA2A8058D1C630608EE98886B595A1DC8EB991DC021E4B9ECA8757958EADD280ACB27815F760E25DE253DB6203A289C94F51A98ED052538D313EB56854554465D1D5A8A6F09392622A39316575C8B9A87858662228A0334F6595E8A8E6BAF3696A6AB4E2DC54536ECA6A90F380E9E8389BD83865B56839D246E3EB94B1FE49BC8E7AD34C2CAEA2A3DD001A4E1DEDA6E4D67F3B718E4DAD2910D88DD8B1B57167B6356F897B146EB5F7D6EDFE3931EDF52BF7D106BC99B785EC52E29685BB683A01300183BA9DDB5F773BD258DAEBB47B972C782AC5CB58682EBA8CF0D952F1EB294CFD790DB18F3023E832B35092E738BA8AABC930D7A22A0BFF5E22CC8087A6A878705F0137438F5D98A6F9573ACB8FF13D0B2FD15A3D3A5D668B6586BA0CC3CB80F1E67852ADAB3F27E364DBBC7FBAC8BF99DB471750337D7C1C771A3D59FA8147DA7D24398E5340E0D97A79D68A7599E133D7F98A20BD10AE40AB804AF19145C6390C1701BEBE741ACDC01BD8A66DC85C9FC3397057D59D3A3548B32258B1EF3FF5C13C01615A62D4E5D14F64C35E78F3C1FF01E094560A819D0000 , N'6.1.3-40302')
