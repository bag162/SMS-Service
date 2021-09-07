
local data = {}

local uuid = require('uuid')

function data.loadDevDataUsersDB()
    local users_space = box.space.users

    users_space:insert
	{
	uuid.str(),
    'vpancc',
	10,
	'123456789'
	}
end

function data.loadDevDataOrdersDB()
	local orders_space = box.space.orders

    orders_space:insert
	{
	uuid.str(),
    0,
	'19495233519',
	'',
	0,
	2,
	'',
	'',
	''
	}
end

function data.loadDevDataHistoryDB()
	local history_space = box.space.history

    history_space:insert
	{
	uuid.str(),
    1,
	uuid.str(),
	'2021-08-10T15:31:58.4762167+03:00'
	}
end

function data.loadDevDataServicesDB()
    local service_space = box.space.service

    service_space:insert{1, 
    0.5, 
    '{"Expressions":[{"RegularExpression":"вот123 таsgasк вот"},{"RegularExpression":"вот т32asghasg3ак вот"},{"RegularExpression":"вот т4asd5ак вот"}]}',
    "vk"
}
	service_space:insert{2, 
    1, 
    '{"Expressions":[{"RegularExpression":"вот123 таsgasк вот"},{"RegularExpression":"вот т32asghasg3ак вот"},{"RegularExpression":"вот т4asd5ак вот"}]}',
    "tg"
}
end

function data.loadDevDataAccountsDB()
    local account_space = box.space.account

    account_space:insert{
        uuid.str(),
        'elisandoval2016',
        '7w2vzd6op3',
        '19495233519',
        '{"cookies":[{"domain":"www.textnow.com","expires":1631317723.8552661,"httpOnly":true,"name":"connect.sid","path":"","priority":"Medium","secure":true,"session":false,"size":99,"value":"s%3AsM6eg4iLV39gOXRn8hrShPeIe-JRFsJ1.GJKVlZy9G9RrfxJEUU%2BS3KmNs%2FhVjBOAX%2FRU%2FPrtoFM"},{"domain":".textnow.com","expires":1691797715,"httpOnly":false,"name":"_ga","path":"","priority":"Medium","secure":false,"session":false,"size":29,"value":"GA1.2.299329929.1628725702"},{"domain":".textnow.com","expires":1628812115,"httpOnly":false,"name":"_gid","path":"","priority":"Medium","secure":false,"session":false,"size":31,"value":"GA1.2.1558183229.1628725702"},{"domain":".textnow.com","expires":1628725762,"httpOnly":false,"name":"_gat","path":"","priority":"Medium","secure":false,"session":false,"size":5,"value":"1"},{"domain":".google.com","expires":1644536902.7602229,"httpOnly":true,"name":"NID","path":"","priority":"Medium","sameSite":"None","secure":true,"session":false,"size":178,"value":"221=b-rI67bcWcSkyaDuMdl8m3vKQcpggI_nsjORCPCUW890LY71U77va-RBMjA_q1KktLRSaG2-GlRq99EaGgEhIYQj2pGCKUcAXM_3ldkw59pKsmoivezkysdg8yt4I1t-cZ2nC4YngqKr4Ll89wMtpLrB-gjNQnP4R1PvRC0ElpU"},{"domain":"www.textnow.com","expires":1936309702,"httpOnly":false,"name":"tatari-cookie-test","path":"","priority":"Medium","secure":false,"session":false,"size":26,"value":"18132909"},{"domain":".textnow.com","expires":1628726002,"httpOnly":false,"name":"t-ip","path":"","priority":"Medium","secure":false,"session":false,"size":5,"value":"1"},{"domain":".textnow.com","expires":1936309702,"httpOnly":false,"name":"tatari-session-cookie","path":"","priority":"Medium","secure":false,"session":false,"size":57,"value":"e5631fe3-67d8-498c-19e3-c3e78ce8d91c"},{"domain":".textnow.com","expires":-1,"httpOnly":false,"name":"pxcts","path":"","priority":"Medium","sameSite":"Lax","secure":false,"session":true,"size":41,"value":"9d0611f0-fafe-11eb-93da-337c47ccc8b2"},{"domain":".textnow.com","expires":1660261702,"httpOnly":false,"name":"_pxvid","path":"","priority":"Medium","sameSite":"Lax","secure":false,"session":false,"size":42,"value":"9d05a380-fafe-11eb-b4ad-56686e76494e"},{"domain":"www.textnow.com","expires":1628726002,"httpOnly":false,"name":"_pxff_tm","path":"","priority":"Medium","sameSite":"Lax","secure":false,"session":false,"size":9,"value":"1"},{"domain":".www.textnow.com","expires":253402257600,"httpOnly":false,"name":"G_ENABLED_IDPS","path":"","priority":"Medium","secure":false,"session":false,"size":20,"value":"google"},{"domain":".textnow.com","expires":1636501704,"httpOnly":false,"name":"_gcl_au","path":"","priority":"Medium","secure":false,"session":false,"size":32,"value":"1.1.2113963538.1628725704"},{"domain":".intljs.rmtag.com","expires":1660261704.767664,"httpOnly":false,"name":"rmuid","path":"","priority":"Medium","sameSite":"None","secure":true,"session":false,"size":41,"value":"eb0ac922-7983-4b9c-b17c-54f5837e1aa1"},{"domain":".intljs.rmtag.com","expires":1660261704.767807,"httpOnly":false,"name":"icts","path":"","priority":"Medium","sameSite":"None","secure":true,"session":false,"size":24,"value":"2021-08-11T23:48:24Z"},{"domain":".linksynergy.com","expires":1660261706.5617449,"httpOnly":false,"name":"rmuid","path":"","priority":"Medium","sameSite":"None","secure":true,"session":false,"size":41,"value":"b0a07ea9-44c1-4640-b4e5-11afaebcbe5e"},{"domain":".linksynergy.com","expires":1660261706.561923,"httpOnly":false,"name":"icts","path":"","priority":"Medium","sameSite":"None","secure":true,"session":false,"size":24,"value":"2021-08-11T23:48:25Z"},{"domain":".textnow.com","expires":1660262065,"httpOnly":false,"name":"stc117823","path":"","priority":"Medium","secure":false,"session":false,"size":269,"value":"tsa:1628725705399.1034693386.497262.4500821018241574.3:20210812001825|env:1%7C20210911234825%7C20210812001825%7C1%7C1073241:20220811234825|uid:1628725705397.275118279.33830166.117823.17816407.0:20220811234825|srchist:1073241%3A1%3A20210911234825:20220811234825"},{"domain":".rlcdn.com","expires":1660261706.0208349,"httpOnly":false,"name":"rlas3","path":"","priority":"Medium","sameSite":"None","secure":true,"session":false,"size":49,"value":"V32Xffuedv3qEZVENs0I6++dCbRvClqyJ5wOmTSEnYw="},{"domain":".rlcdn.com","expires":1633909706.0209799,"httpOnly":false,"name":"pxrc","path":"","priority":"Medium","sameSite":"None","secure":true,"session":false,"size":32,"value":"CMnD0YgGEgUI6AcQABIGCOTrARAA"},{"domain":".textnow.com","expires":1636501719,"httpOnly":false,"name":"_fbp","path":"","priority":"Medium","sameSite":"Lax","secure":false,"session":false,"size":33,"value":"fb.1.1628725719427.1075144834"},{"domain":".youtube.com","expires":-1,"httpOnly":true,"name":"YSC","path":"","priority":"Medium","sameSite":"None","secure":true,"session":true,"size":14,"value":"SGYqr_-VyRE"},{"domain":".youtube.com","expires":1644277719.6010339,"httpOnly":true,"name":"VISITOR_INFO1_LIVE","path":"","priority":"Medium","sameSite":"None","secure":true,"session":false,"size":29,"value":"dGaQZ_nmvF0"},{"domain":"www.textnow.com","expires":-1,"httpOnly":false,"name":"language","path":"","priority":"Medium","secure":false,"session":true,"size":10,"value":"be"},{"domain":"www.textnow.com","expires":-1,"httpOnly":false,"name":"XSRF-TOKEN","path":"","priority":"Medium","secure":false,"session":true,"size":46,"value":"LkK5VsEp-EDPOFUDe912hRrmVdhFq1lcp298"},{"domain":".textnow.com","expires":1628726060,"httpOnly":false,"name":"_px3","path":"","priority":"Medium","sameSite":"Lax","secure":false,"session":false,"size":379,"value":"d45ccdeadb925d7099d7fd0d8a80c4a3d8e1b3fd18d84170cda4c31428fc899a:hXC3ovDwobYd5+LF5nee6lqMEAEv6OuCmXdUmILzMdea0vTLpFKEYulQG7DY12JMzU0p8cXOXEew1DvbLMBTfQ==:1000:HamjUfLoE9DQMnxE9jIpgFxpDP1u4DTZkoWGX6uFsUTGUf88mPf4Z42CRYejbG5fRXfp6r6No9zFvXe8+8xU9QvXrFmD+doBrAlRsd6pd+HX6wznaz+DyBHbaKgPeYjGYlAFEAHWhM4NTCI5Dl3XSH+6w72a6qin+R6Bj++6CVWEvDpOHx7vPv9AD2ajY1ZvJAisnPNnR1ESxuwRltVw=="},{"domain":"www.textnow.com","expires":1628726630,"httpOnly":false,"name":"_dd_s","path":"","priority":"Medium","sameSite":"Strict","secure":false,"session":false,"size":31,"value":"rum=0&expire=1628726630389"}]}',
        1
    }
end

function data.loadDevDataProxyDB()
    local proxy_space = box.space.proxy

    proxy_space:insert{
        uuid.str(),
        'zproxy.lum-superproxy.io',
        '22225',
        'lum-customer-c_518000c3-zone-zone17',
        'bag16222',
        1,
        '0',
        '0.0.0.0'
    }
end

return data;