ALTER TABLE [dbo].[Articles] ADD [Selected] [bit] NOT NULL DEFAULT 0
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202307100943413_unknown', N'IslandOfHealing.Migrations.Configuration',  0x1F8B0800000000000400ED5DCB6FE44819BF23F13F587D02349BCE635682A8B3AB999EC912983C349D59711B55EC4AC75A3F1ADB3DA485382201121217C4110E1CE08010B79590D87F8619B8F12F50E547B9DEAEB2DD7667148D344ACA555F557DF5BDEAF1FDF2BF7F7E33FBFC3E0C9C773049FD383A991CECED4F1C18B9B1E747CB93C93ABBFDE4FB93CF3FFBF6B7662FBDF0DEF9B2AA7784EBA196517A32B9CBB2D5F1749ABA773004E95EE8BB499CC6B7D99E1B8753E0C5D3C3FDFD1F4C0F0EA6109198205A8E337BBD8E323F84F92FE8D7791CB97095AD41701E7B3048CB72F4659153752E4008D31570E1C9E42C0D40E45DDEFE1082000D72AF6831719E053E40A359C0E076E280288A3390A1B11EBF49E1224BE268B958A102105C6F5610D5BB05410ACB391CD7D54DA7B37F88A733AD1B56A4DC759AC5A125C183A3923F53BE792B2E4F08FF10075F224E671B3CEB9C8B27936749E6BB019CC76108A36CE2F05D1ECF83045757B17A8F25F0C4E1AA3D216282A409FF7BE2CCD741B64EE04904D759028227CED5FA26F0DD1FC3CD75FC158C4EA27510D08346C346DF9802547495C42B98649BD7F0B69CCA993771A66CBB29DF9034A3DA94D38BB2A3C38973813A073701243241B16291C509FC0246300119F4AE4096C124C23460CE55A177AE2FC2E3A2432487883F13E71CDCBF82D132BB3B99A0559B38A7FE3DF4AA9272106F221F69216A94256B2819A4BEE3B3C8CF5EA021573DE39FAF91C2591342DA9334734C4FA394176B3217E09DBFCCD7812378BE29494E9CD730C86BA477FEAA50FE4A3CDFB2628A2CC4691287AFE3A01E115FE5ED354896102F57ACAFB788D789CB0D7836AD35CD44FFDA2BDEA3C6A9FBBAF6B3006AF4EDA999BA35A97594E9D51AFDD84B3FC831BF4982AD77740536551FCF6324F9206AABE4E91C2DDB324E365D8DC6621D8620D968A67ED4C7525E8365BA7DF626F1328169DD11365375E158B6FD95FF15BC5887DDD6691E070174B3CE74FA703488AF6828D0B39665A5A7117C48BFFEA6F2234DFEA6F24BD60E92A8A36EE4A452D5AF6CEC622595B794D494F94BFDF8B13848C78C3F48C7C97C10C6C67EEDC37F53AC6DEBC80989478FAEEE0BFFAFB1D0079FEE56FCDC644AB436A4AD262AEC8846675B497EA194D6D28E9B3D4AB8BAAF67AE1BAFB5E1A461D46A29E457204D7F1627DEE01D2F40D047F06CD9EB85EF7ED5604C7AD91D9C85CB216276ACE983DBC5E77E92DD79F56EA1B68BFA76E79BAB00076243AFF98FE29BA61DA121A39AF8126F7DC1BF58FB3A55EDA793DE36179571291A37484B3BA769180AF2CE511E28B67288E5E6076FA45AF845AAF5A37B54F735E611A464C7DCF5A004D3906B98D146BB6BF72519A311B43C80A5E4FA2D55B3D64F6905414FE5B53AE9EB65E2B58A60F3768F3AAAEEABC1376D278C9C073E1A5D3F215DABAE5F86C0D7057ADBECFBEA2E8E1ED2BE58080F7CAF8D157C19B56A86A3CF510405777C95F82EECE678FAF081781868D278449D4901BFC74356B439A88726F528B9FD7DCBD5ABFD89E4B3E04D6475EC8F5595C79215F9A28238B43CE8548D29FFD8C9B1318CB1766F54EB4727A7EE6B1403720A0166B6EE92EAE0D33E8E2D7AB052CFFD008BCC7CE36A77DC5BDAEBB7F44D6A1D5B878C8655D77567E9690096F513A216EA5690EA4DD710E3902109368804BD7C2CB7CE617803937236FFFDEDAFFFF3976F26CE972058A35FF705DE32B5DFFFEB97EFFFFED70F7FFA9A3438686890D7FEF7D77F230D0E4D1ABCFFF33F3EFCFE0FA4CD91499B0FBFFADDFBDFFC91B4792A2E6EB18C74E1B3348D5D3F5F2C767F28DCE1B1DDA3E0C331BCD0ABEF79C9FEE71C2DA5BF428B87840D7390378897D10B18C00C3ACFDCE2E1D81CA42EF044B94773F26C0746EEC3848191374AECF8BE27748B0C354CB0A504C11CF93E248E7E948956DD8F5C7F0502334671CD0DDD02E601E988FFF202AE60842DBA19474C46409D1E880321FD718BD4C4AED994124323E994DC0E358881EEAA481004EA42734851D55D2C376A51BF52AA66D77072AA668785A432AF70461159F66C5625028A83DA7AD98B707A107154BC1F184A04A5AC1840ECA4D336E9B7DA928E225EF22346D5D2369C37D64BCC9CE7372D73137D7311EA55A25B499F76060348A176851E828B969D50A8C4457B5C510B4B79586D2E86BA230EE6E1217556B06B82A899C30062A8591993DEB913B6510531F79C4D92C29E4AF5207ACC49D6907EBC93B0D1A31E50CA68EEEF98C72D8E44F03B77D482ECB8F377EFF7B2942134B4F2D4232D4F6F7849C1F4163053BDABADCF6014BB5341F4A40435948C4994B1B3AF2346ED951AA8E24593112A34A2A131E51665349870A581542E743222A5AE3734A74C9B8C08E3533852948809CC161F4D53B51BDE57F35A607E3A432647CB8DA055E6872A22BD7A3EBCE562B961CE29D9D35025AF9A4E0A2CCF0A24FCA295A499739A3DBEC15AB4601AF734486494667F6AB043A5065DEAB78607F28DE576E6AD787A21CEDF600365B185A226C39A2C0D5BF49BA6EDB0477A8F2832A73198370EE7A969540658C3125D004F51626D716F4C293C929A1B6244D91C53B69B3F134536E99A66C6D5D50E8963C8B7D9B4482C2F0B66534506FAEC1CAC5688D754467A59E22C8A74F4F9270BFB1CEDB0A0317519D3C3475DA4A72C4EC012725FB10679F0D44F527CEB056E00BE8B9A7BA1A45A11B5291C7CD58B3C301317ABF2FC553BFC7319169BA48B4B22DD92D2299A24AE91CF172A7DAA48C0C13001200089E46E1859997518A9C376756B1279D22414E1A87614E45A92190B2935A754C5FF341DD59E404D853A29A109690E50B07E706B24EC4004B910366CACB8D908639F52D85EFC8692BBF2BD3A4DA02CB291DD32419995DDB2D0864E9580CC12AA4ACD29E519C63491BCC05A6A998B0D89F86A2F3ED4D449B2314D92145AAC5D9E4DCC2C5D5E62C127F2CC806116291DC3EE903C619A1029B491A63A559895A7BA7C686B58A70C334B4F4A77CE16521BAFFEAC627D9CD1DA3C6A486CC74E16AFB1E8F645C9363564A4B52FE2DE1E965B16C21B2CB1BCD9769695244332D6BD2AB47138557223EB75AA520B1391672B32E6212FB110559279C8882B29B510D932B790E16F59664EA5481DA4691425E614EA3C409A4A5D6A4EA9CA0CA4E95465E654EA3C3F9A4E5D6A33B3989F546CD3BEC884A009142563386E36074FA607B2B7F339BD710C1D7378D483BDA30FC8EDCD9EB6F576ACDF785B3C5DF027467EB6144479AE4BAD034869F4D8828E3828E6C3CE68457972D6833E14773DF69AA068B71D1DE86E41E9C4306679A9725B6A65AE9748AEFC604BAFCCDF12E9951FC6F117555216EB2BAA52734A244D8B26440A2D4644F2B6981191523B4A658A034FAA2C1EDA4A73AF45D8FDBEF621897E05C5D5DB215BC6DC5DF460D134573406764DDB7A57B7AD7546104DA52EB593405E1F6C7581CDF9610367FACB83DD960B97487C15D23BB94CE22E8D66E5054E33B6B170A3535499388845EF7C0FDFE62C366906C33D5C616FF1D3A0F01975857310F9B730CD8AECA0C9E1FEC121078DBC3B30C5D334F502C905981AAB985DB501920A7DCCDBC6B4416B08060615387A0712F70E24222E70C704380FFD9C7507FDCD79D0156FC397224D9F451EBC3F99FC3C6F75EC9CFDE42D69F8C4C983CE6367DFF945EB0C3E05F2EEC721460CB09154889E3232D402D9B6A2FA9D10DC7FD79E168B5EDB891885507BE3B79647093AADA56052142C24541C10076F2B5DBE23EBE5A3D16CBBB19B43AC6D63037A33471C4E6D9BB18828B56DA848ECA281E414ADBA490B076CDBAC02EDD14C3F0EE35804D51AE5C2F828C308B41DBCE6C7C17E0ED2D2C03BB542ACEC8B2E0D48A9B49BAD40267BF2CB2CA66427D34EE346F6A5193C2CA44C334CC6C6C244765B091EFAD160AE96488F9D968146CCEA44A837372B436C345BC85678891F87A17B605B257970D525B2E6E10CDBAA3E076FD866281268C39EE557720FF26025576680BAFA511191AF5FCA0CE05EBFA4193CBD912346996D66E1F2DA6A19079FD7960C0FA7D75B7CC7A3E53DA85D9A1460CFB47FA671B76150E07C3D6E14D599ED0FD5026E438279B8368525B1DE0074D60A19365BB700BBE7ED706FA05CE3A36FF17FF66F68B4AD01E18AD4593D628F6363747487D1DA21D8AC11D1B10614AF8F15F46A2C90ABE18547F1E47C54817940D055E3FB340664A22D56D6AE7B33FD43E1DD73653670534CB2FB0838505C7E771BA0A156E2A37F1AD6BB08A9DED68A1D3D1898A8313DD5B0B23294973217921D007BE2D105F8A5D2413A19203A156FDBD0BEF106DFA814FBC526F81C45A73AD4274D37C6F4A9185FD5115545D7A31A37478A1CA5048E92F52187C7E0C9327E5BA0CE7C9575A2C37791834EA931A764F4E530213A3CAA26382A592F5A0895D100AB64B037120BA03E6F109BABF0D31E000E950E774A9262D88E9D63634B09F836234E6C6BE0516DC55B62AA84C4BCEED3DE0A2894C43E091907BAA9339693CAC1EA6FBA1DE19EEC85B7FB942CF09CC427F728405A47F87CBAF8ED054CFD654D62866846D065422352E72CBA8DAB088D1B5155857FC80233E0A1B8094BF92D7033F4D985699AFF2594F22F55BC0C6FA077165DAEB3D53A435386E14DC098351CE9E9FACF41ABD831CF2E57F9DF25EA630A68983E3ED2BF8C9EAFFDC023E33E951CE92B48E010B2BCAFC16B99E17B9BE58650BA1012DD54844AF691C8F71A86AB00114B2FA3057807DB8C0D89EB2BB804EEA6CA9C5013695E0896EDB3173E5826204C4B1A757BF42B92612FBCFFECFF12B677DD7B8B0000 , N'6.1.3-40302')

