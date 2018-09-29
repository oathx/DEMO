module("RpcParse", package.seeall)

rpc_server = {}
rpc_client = {}

function parse_function(funcimp)
    local rf = {args={}}
    for t,n in string.gmatch(funcimp,"(%w+)%s+(%w+)") do
        table.insert(rf.args, {t=t, n=n})
    end

    return rf
end

function parse_protocol(protocol)
    local server_func = {}
    local client_func = {}

    for k, v in pairs(protocol.Server) do
        client_func[k] = parse_function(v)
        client_func[k].id = uvcore.hash_string(k)
        client_func[k].name = k
    end

    for k, v in pairs(protocol.Client) do
        local rf = parse_function(v)
        rf.id = uvcore.hash_string(k)
        rf.name = k
        server_func[rf.id] = rf
    end

    rpc_server = server_func
    rpc_client = client_func
end

function dump()
    for k, v in pairs(rpc_server) do
        print(k, v.id, v.name)
    end
end

function cf(rtype)
    return rpc_client[rtype]
end

function sf(rtype)
    return rpc_server[rtype]
end
