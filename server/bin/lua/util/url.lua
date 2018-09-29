function encodeBase64(source_str)  
    local b64chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/'  
    local s64 = ''  
    local str = source_str  
  
    while #str > 0 do  
        local bytes_num = 0  
        local buf = 0  
  
        for byte_cnt=1,3 do  
            buf = (buf * 256)  
            if #str > 0 then  
                buf = buf + string.byte(str, 1, 1)  
                str = string.sub(str, 2)  
                bytes_num = bytes_num + 1  
            end  
        end  
  
        for group_cnt=1,(bytes_num+1) do  
            local b64char = math.fmod(math.floor(buf/262144), 64) + 1  
            s64 = s64 .. string.sub(b64chars, b64char, b64char)  
            buf = buf * 64  
        end  
  
        for fill_cnt=1,(3-bytes_num) do  
            s64 = s64 .. '='  
        end  
    end  
  
    return s64  
end  
  
function decodeBase64(str64)  
    local b64chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/'  
    local temp={}  
    for i=1,64 do  
        temp[string.sub(b64chars,i,i)] = i  
    end  
    temp['=']=0  
    local str=""  
    for i=1,#str64,4 do  
        if i>#str64 then  
            break  
        end  
        local data = 0  
        local str_count=0  
        for j=0,3 do  
            local str1=string.sub(str64,i+j,i+j)  
            if not temp[str1] then  
                return  
            end  
            if temp[str1] < 1 then  
                data = data * 64  
            else  
                data = data * 64 + temp[str1]-1  
                str_count = str_count + 1  
            end  
        end  
        for j=16,0,-8 do  
            if str_count > 0 then  
                str=str..string.char(math.floor(data/math.pow(2,j)))  
                data=math.mod(data,math.pow(2,j))  
                str_count = str_count - 1  
            end  
        end  
    end  
  
    local last = tonumber(string.byte(str, string.len(str), string.len(str)))  
    if last == 0 then  
        str = string.sub(str, 1, string.len(str) - 1)  
    end  
    return str  
end

function url_parse_header(chunk)
    local header = {
    }

    local results = tools.string_split(chunk, "\n")
    for _, s in pairs(results) do
        local str = tools.string_trim(s)
        local arr = tools.string_split(str, ":")
        if #arr >= 3 then
            header[arr[1]] = string.format("%s:%s", arr[2], arr[3])
        else
            header[arr[1]] = arr[2]
        end
    end

    return header
end

local url_header_val = {
    WebSocket = "websocket"
}

local url_header_key = {
    UPGRADE = "Upgrade",
    WEBKEY = "Sec-WebSocket-Key"
}

local const_sha_guid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"

function url_hand_shake(chunk)
    local header = url_parse_header(chunk)
    if not header then
        return
    end

    if header[url_header_key.UPGRADE] ~= url_header_val.WebSocket then
        return
    end

    local key = header[url_header_key.WEBKEY]
    if not key then
        return
    end

    local key = tools.encodeBase64(uvcore.sha1(string.format("%s%s", key, const_sha_guid)))
    return string.format("HTTP/1.1 101 Switching Protocols\r\n"..
            "Connection: Upgrade\r\n" ..
            "Sec-WebSocket-Accept: %s\r\n"..
            "Upgrade: websocket\r\n\r\n", key)
end
