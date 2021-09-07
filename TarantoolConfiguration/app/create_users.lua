local users = {}

function users.createUsers()
    box.schema.user.create('worker', {password = 'FuEog86YW3Nnkjhg2WLC', if_not_exists = true})
    box.schema.user.grant('worker', 'read,write,execute,create,drop', 'universe', nil, {if_not_exists = true})
end

return users;