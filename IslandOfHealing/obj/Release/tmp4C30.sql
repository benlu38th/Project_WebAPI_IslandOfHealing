EXECUTE sp_rename @objname = N'dbo.ArticlesClasses', @newname = N'ArticlesCategories', @objtype = N'OBJECT'
IF object_id('[PK_dbo.ArticlesClasses]') IS NOT NULL BEGIN
    EXECUTE sp_rename @objname = N'[PK_dbo.ArticlesClasses]', @newname = N'PK_dbo.ArticlesCategories', @objtype = N'OBJECT'
END
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202307051226099_AdjustArticleCategory', N'IslandOfHealing.Migrations.Configuration',  0x1F8B0800000000000400ED5B4B6F24B711BE07F07F68F4C90964B5A4B5014798B1A19D5DD963AF1ED8D11ABE2D38DD9C11B16C72D264CB1A043906880304C8C5F03139E4901C8220370301E23FE35DFB96BF10B21F6C36FBC59E698D8485B1C042AA667D2C56158B4556E97FFFF97EF4F16D889D1B183144C9D83DDC3F701D487C1A20B21CBB315FBCF7A1FBF147EFFC62F434086F9D2FF2718FE438C149D8D8BDE67C75EC79CCBF862160FB21F223CAE882EFFB34F44040BDA383835F7B87871E1410AEC0729CD1F3987014C2E417F1EB84121FAE780CF0190D2066195D7C9925A8CE3908215B011F8EDD29C38004178B4F21C042C8FD94C3754E3002429A19C40BD70184500EB890F5F80583331E51B29CAD0401E0ABF50A8A710B8019CCD6705C0CB75DCEC1915C8E5730E6507ECC380D7B021E3ECAF4E399EC1B69D955FA131A7C2A34CDD772D58916C7EE49C4918FC5DACDB98E273892E39A74BC9F71EE39C6F73DE518C27FE4BF3D6712631E47704C60CC2380F79CCB788E91FF395C5FD157908C498CB12EA610547C2B1104E932A22B18F1F573B8C8849F06AEE395F93C9351B1693CD9BA087F74E43AE7627230C7507981A68319A711FC041218010E834BC0398C88C480891E2BB31B735D212E559B4E27FC4E68C775CEC0ED334896FC7AECBE2FF6CD29BA85414EC824784190D87482874731EC9A446C172EC4699946FC38C83C2230BC88F09D4F7409D6F91C8F29C510901A23B54364BEC9261830D66DF176B0591C86205AB7AC5BECB601D67D0596ECCE959B6966261C3C2E66136172BFF445D27B2B6A4A107F22364A0E2B7FBE1281BD37D033F40A9EC7E176669B508CA1CFB7C6114746D4DB85CEC10D5A2631C4403B5B977CD3759E439C8C63D76855B6049B08FD2D69B47E991384FD231A3EA758777173D0CB2B102DA18C07B46BE48CC691DF4B78A98C5A99E5875A394B1F2AB295BFD6C933F28A33CBE62453CBDCFC4853103F9F6DCD73C9FF5BA2D5E10756C16A6711A6D1A70B9F1D7E27E6FEDCBD13F37DB191E7A79BB2B7B74BB69F3DBC79AE13DFA7716B626599BFF574F24B712E7C45A360E713CF001E228DEC39EB39F25F75049341F2E469B8DC45F62A77FACEE3E26314F1EBA0C89B8BB8D8CE77B6BEC432BDDEB5CD3FA3F3AEBB91A5A2BAF442EFDCE09FC4A86DAB0E33C960A9751E5C52E60E6FD9ECD0B44C05CDC3B13E51B43B10E3B09A08EA1799293BC5E276A574B4696658600E76700A3F086084D702423FD8CA863883E11C46D9FA7EFAD3D73FFEFD7BD7F902E058FC7A50315B69F4EBFFFEFEF5BFFEF1E6AFDF2986C30E8664F40FDFFD53311CD930BCFEDBBFDF7CF3ADE27964C3F3E60F7F7EFDC7BF289EF7ABE64E0DAB134F18A33E4AACD690F817BE5716E029091CEB8CAD722FD6EE1567C2AE68252C297C51AAD34C752EC813882187CE899F3EFA4D00F34150DD5F6281417F19D51DAA22A329DAAF2A338AEC0B4632FD01782236AD704B4478355543C4472B806DD5650058667B52016A2AF3CB13B88244266AB6EAB091A1F22C541548CD6B58AA4B71234F73CC767F2DC7C726FB3704CBC2E669D6BF135F6CB8C3EFCAFF6A55B1039FAB5DB6CDBCF993D10EDC2B3D11E51BB0E050E1357913BEE535079D102D3BEB5896799846977833C82B277D71F69A46F7EC20B2AD8BDAC0B428DB812A755C07946E0B8359535EA35CFAE55E1BDFF91260DAB8CF31A31655AFA58AFFF4391E6AB0AB9BBEAC180BA519C95D55512DD1CD22BE694267066ED1417D58DA6EDD798EA97654517DF4D2F2635EA6F41AEA94A333B05A896C4EAB5B66146796162D27EFCDFA57F2C214C3F3594D414F49AB66E234024B687C95812180A72862F23201E640E6C29320AC1996C68F862D98CF628488AAA5F2BD9933C89FB3D0DC9A67D744D90CE2542C2B94213A79DCA9DABACA99D48D010651CD53D284E23824CD87453377767DD50132923D86AADCE9288AD80727AFCC958172AA3D52527AD34112823D7F25C1D2B13AB3AF665C5582D3F114B187D592125BC96809A5F70AF3FA59CDFAF24FF698C5E5BEE48F8A6A8FA40A673A9022F6F1A8A27656F6A9826E8F96A7433A52538A2403ABB1D52B495425AE5432D9729CEA15C5B4F377B87856A4351B07B61688BB8970E99BACCE9F52EED2AFEFC9F669AA3180B993E4B3BF89EBD9EEC6ACAAAA510A5D39B1CF51915729CAE7454EED11DC93B24329B227941EAEAA4A08257755D41E2E9B15094AFACD68F628690D40C74829F608C583BE8E5250ED91F2277E1D27A7D9A3140FF63A4E41EDB3326A2E8AF6E14F9FDB758094721FC76DF931BD6E1FD43DAB27787719E82A771873889A5DDD658C3BCB28BB3F743760562E14E910D711AABA4181BC4CCCD68CC3705F0ED89FFD064F304AF2DB7CC019206801194FDFC4DDA383C323A37FF3E1F4527A8C05D8AEA172E705712495DA59F2EE592E2A55E9C80D88FC6B10556AB305E8060D8B39EABB21B8FD657FAC7253E256605AE3E11CF1A19A0E13BB54DEE1A62480B763F7B709EFB133FDF2A5C1BEE75C4462771C3B07CEEFB66D59AC355CD2B4D84F417A87E2569AAEED424C14B5654350207EE603B41C6E224BB5E170139472BBA1ADF3A45C3D7C66F3B6BAB723B0E91D27B5DB43F61FECC619FBF579BD1DEA377AAB2C4E968D5AA786C2D53BA31A03DF46DD4E039DA9E5E6A6AD62B3DEC034D4CE30FB93EA76868D6CE57EA5ED2C61F62059ACB567CBD15666D0DB8AB6021AEC88AC6B1DB233645384BB8B5E8E07D4BB718F2D1A3B6CC9782B3B2FEEABD362F79ED3F06479AFDEF200FA27CC3269C33BB271E96F699248DF4544C49CCBF3218D948DF5E98E168AB5550345DB8CCD5D06B58D168D7D167573D416EE1F480B465BCB45CDB36ADDCEACA9393FB0B68A4A1BC50E16D6A36FA2FAB628F6AFF627E0227430B42C20E41F8413E89776AE1A33250B9A071043A27C889943420E44DA02E41A17C0E7E2B30F194BFAB6B346D4A7E11C06537211F355CCC5926138C725EF9081A86DFEA439A42CF3E86295F4490FB10421269299D705791C231C28B94F6B2E970D1032C265F739694B2EEF75CBB5423AA7C41228539F0ACC57305C6101C62EC80CDCC04D64132EFB0C2E81BFCE9F889B41BA0D5156FBE80902CB08842CC328F8C5AFC28783F0F6A3FF03F6D3780F09410000 , N'6.1.3-40302')
