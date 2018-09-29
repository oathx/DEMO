local function _rpc_init(rpc, server)
    rpc.server_     = server
end

local NumberType={
    float=true,
    int=true,
    word=true,
    byte=true,
}

local function _is_typeof(i,s,v)
    -- such as string type
    if s=="string" then 
        if s==type(v) then return true else return false end
    end
    
    -- if it's a number type
    if NumberType[s] and type(v)=="number" then return true end

    if type(v) == "nil" then
        return false
    end
    
    -- if it's a user defined type
    local ut = RpcType[s]
    if ut then
        -- if has check function
        if ut._check then
            if ut._check(v) then
                return true
            else
                error(string.format("bad argument %d, expected %s, but got %s",i,s,type(v)))
            end
        else
            return true 
        end
    else
        error(string.format("can't find user defined type %s, argument %d",s,i))    
    end
    
    return false
end

local function _rpc_make(rpc, key, ...)
    local cf = RpcParse.cf(key)
    if not cf then
         error(string.format("can't find client function named %s",key))
        return 0
    end

    local packet = uvcore.new_packet(4096)
    if packet then
        -- skip 4 byte, write packet length 
        packet:write_int(-1)
        packet:write_int(cf.id)

        local args = {...}
        for i, v in ipairs(args) do
            local t = cf.args[i].t
            if not t or not _is_typeof(i, t, v) then
                error(string.format("bad argument %d, expected %s, but a %s",i,t,type(v)))
            else
                RpcType[t]._write(packet, v)
            end
        end

        return packet:to_bytes()
    end
end

local mt = {
    __index = function( table, key )
        return function(rpc, cl, ...)
            local data, len = _rpc_make(rpc, key, ...)
            if data and len >= 8 then
                print(data, len)
                cl:send(data)
            end
        end
    end
}

local function new()
    local instance = {
        init = _rpc_init,
    }
    setmetatable(instance, mt)

    return instance
end

Rpc = new()