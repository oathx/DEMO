RpcType = {}

RpcType.int = {
    _write=function( packet, v )
        packet:write_int(v)
    end,
    _read=function( packet )
        return packet:read_int()
    end
}

RpcType.uint = {
    _write=function( packet, v )
        packet:write_uint(v)
    end,
    _read=function( packet )
        return packet:read_uint()
    end
}

RpcType.float = {
    _write=function( packet, v )
        packet:write_float(v)
    end,
    _read=function( packet )
        return packet:read_float()
    end
}

RpcType.string = {
    _write=function( packet, v )
        packet:write_string(v)
    end,
    _read=function( packet )
        return packet:read_string()
    end
}

RpcType.pack = {
    _write=function( packet, v )
        packet:write_block(uvcore.cpack(v))
    end,
    _read=function( packet )
        return uvcore.cunpack(packet:read_block())
    end,
    _check=function(v)
        return type(v) == "table"
    end,
}

