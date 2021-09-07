local spaces = {}

function spaces.initUsersDB()	--in-memory
    local users_space = box.schema.space.create('users', {
        if_not_exists = true
    })
	
    users_space:create_index('primary_index', {
        if_not_exists = true,
        type = 'TREE',
        unique = true,
        parts = {1, 'string'}
    })

    users_space:create_index('secondary_login', {
        if_not_exists = true,
        type = 'TREE',
        unique = true,
        parts = {2, 'STRING'}
    })

    users_space:create_index('secondary_balance', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {3, 'INT'}
    })
	
	users_space:create_index('secondary_apikey', {
        if_not_exists = true,
        type = 'TREE',
        unique = true,
        parts = {4, 'STRING'}
    })
end

function spaces.initOrdersDB()	--in-memory
	local orders_space = box.schema.space.create('orders', {
        if_not_exists = true
    })
	
    orders_space:create_index('primary_index', {
        if_not_exists = true,
        type = 'TREE',
        unique = true,
        parts = {1, 'string'}
    })

    orders_space:create_index('secondary_status', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {2, 'INT'}
    })

    orders_space:create_index('secondary_number', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {3, 'STRING'}
    })
	
	orders_space:create_index('secondary_userid', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {4, 'STRING'}
    })
	
	orders_space:create_index('secondary_orderid', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {5, 'INT'}
    })
	
	orders_space:create_index('secondary_service', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {6, 'INT'}
    })
	
	orders_space:create_index('secondary_sms', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {7, 'STRING'}
    })
	
	orders_space:create_index('secondary_sms_code', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {8, 'STRING'}
    })
	
	orders_space:create_index('secondary_start_time', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {9, 'STRING'}
    })
end

function spaces.initHistoryDB()	--disk
	local history_space = box.schema.space.create('history', {
        if_not_exists = true,
		engine = 'vinyl'
    })
	
	history_space:create_index('primary_index', {
        if_not_exists = true,
        type = 'TREE',
        unique = true,
        parts = {1, 'string'}
    })
	
	history_space:create_index('secondary_typerequest', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {2, 'INT'}
    })
	
	history_space:create_index('secondary_userid', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {3, 'string'}
    })
	
	history_space:create_index('secondary_datetime', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {4, 'string'}
    })
	
	
end

function spaces.initServicesDB()	--in-memory
	local service_space = box.schema.space.create('service', {
        if_not_exists = true
    })
	
	service_space:create_index('primary_index', {
        if_not_exists = true,
        type = 'TREE',
        unique = true,
        parts = {1, 'NUMBER'}
    })
	
	service_space:create_index('secondary_price', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {2, 'NUMBER'}
    })

    service_space:create_index('secondary_parseregularexp', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {3, 'string'}
    })

    service_space:create_index('secondary_serviceprefix', {
        if_not_exists = true,
        type = 'TREE',
        unique = true,
        parts = {4, 'string'}
    })
end

function spaces.initAccountsDB() --in-memory
    local account_space = box.schema.space.create('account', {
        if_not_exists = true
    })
	
	account_space:create_index('primary_index', {
        if_not_exists = true,
        type = 'TREE',
        unique = true,
        parts = {1, 'string'}
    })

    account_space:create_index('secondary_loginaccount', {
        if_not_exists = true,
        type = 'TREE',
        unique = true,
        parts = {2, 'string'}
    })

    account_space:create_index('secondary_passwordaccount', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {3, 'string'}
    })

    account_space:create_index('secondary_number', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {4, 'string'}
    })

    account_space:create_index('secondary_cookie', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {5, 'string'}
    })

    account_space:create_index('secondary_status', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {6, 'INT'}
    })
end

function spaces.initProxyDB()	--in-memory
	local proxy_space = box.schema.space.create('proxy', {
        if_not_exists = true
    })
	
	proxy_space:create_index('primary_index', {
        if_not_exists = true,
        type = 'TREE',
        unique = true,
        parts = {1, 'string'}
    })
	
	proxy_space:create_index('secondary_ip', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {2, 'string'}
    })

    proxy_space:create_index('secondary_port', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {3, 'string'}
    })

    proxy_space:create_index('secondary_login', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {4, 'string'}
    })

    proxy_space:create_index('secondary_password', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {5, 'string'}
    })

    proxy_space:create_index('secondary_status', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {6, 'INT'}
    })

    proxy_space:create_index('secondary_lastdatetimeactive', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {7, 'string'}
    })

    proxy_space:create_index('secondary_externalip', {
        if_not_exists = true,
        type = 'TREE',
        unique = false,
        parts = {8, 'string'}
    })
end

return spaces;