require('strict').on()

local cfg = require('cfg')
if arg[1] == 'discovery_disable' then
    cfg.discovery_mode = 'off'
end
cfg.listen = 3300

-- Start the database with sharding
local vshard = require('vshard')
vshard.router.cfg(cfg.cfg)