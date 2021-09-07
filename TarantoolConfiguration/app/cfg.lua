local cfg = {}

cfg.cfg = {
    log_level = 5,
    
    checkpoint_interval = 3600,
    checkpoint_count = 5,

    memtx_memory = 254 * 1024 * 1024,
    bucket_count = 10000,
    collect_bucket_garbage_interval = 5,
    replication_connect_quorum = 0,
    rebalancer_disbalance_threshold = 3,

    sharding = {
        
        ['cbf06940-0790-498b-948d-042b62cf3d29'] = {
            weight = 1000,
            replicas = {

                ['8a274925-a26d-47fc-9e1b-af88ce939412'] = {
                    uri = 'worker:FuEog86YW3Nnkjhg2WLC@194.87.239.232:3301',
                    name = 'storage_1_master',
                    master = true
                },

                ['3de2e3e1-9ebe-4d0d-abb1-26d301b84633'] = {
                    uri = 'worker:FuEog86YW3Nnkjhg2WLC@176.113.83.131:3310',
                    name = 'storage_1_replica'
                }
            },
        },
        
        ['ac522f65-aa94-4134-9f64-51ee384f1a54'] = {
            weight = 1000,
            replicas = {

                ['1e02ae8a-afc0-4e91-ba34-843a356b8ed7'] = {
                    uri = 'worker:FuEog86YW3Nnkjhg2WLC@194.87.248.134:3301',
                    name = 'storage_2_master',
                    master = true
                },

                ['001688c3-66f8-4a31-8e19-036c17d489c2'] = {
                    uri = 'worker:FuEog86YW3Nnkjhg2WLC@176.113.83.131:3311',
                    name = 'storage_2_replica'
                }
            },
        },
    },
}

return cfg;