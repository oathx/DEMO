module("tools", package.seeall)

function strip_file_name(path)
    if string.find(path, "\\") then
        return string.match(path, "(.+)\\[^\\]*%.%w+$")
    end

    return string.match(path, "(.+)/[^/]*%.%w+$")
end 

function string_split(str, delimiter)
    local result = {}
    local from  = 1
    local delim_from, delim_to = string.find(str, delimiter, from)
    while delim_from do
        table.insert(result, 
            string.sub(str, from , delim_from-1 ))

        from  = delim_to + 1
        delim_from, delim_to = string.find(str, delimiter, from)
    end

    table.insert(result, string.sub( str, from))

    return result
end

function string_trim(str)
    return (string.gsub(str, "%s*(.-)%s*", "%1"))
end

base_cid = 1000

function genid()
    base_cid = base_cid + 1
    return base_cid
end

function encode_base64(source_str)
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
 
function decode_base64(str64)
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

function sec_websocket_accept(key)
    local sha1 = uvcore.sha1(string.format("%s%s", key, "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"))
    assert((#sha1 % 2) == 0)

    return encode_base64(sha1)
end

function parse_http_header(request)
    local headers = {}
    if not request:match('.*HTTP/1%.1') then
      return headers
    end

    request = request:match('[^\r\n]+\r\n(.*)')
    local empty_line
    for line in request:gmatch('[^\r\n]*\r\n') do
      local name,val = line:match('([^%s]+)%s*:%s*([^\r\n]+)')
      if name and val then
        name = name:lower()
        if not name:match('sec%-websocket') then
          val = val:lower()
        end

        if not headers[name] then
          headers[name] = val
        else
          headers[name] = headers[name]..','..val
        end
      elseif line == '\r\n' then
        empty_line = true
      else
        assert(false,line..'('..#line..')')
      end
    end

    return headers,request:match('\r\n\r\n(.*)')
end

function upgrade_request(req)
    local format = string.format
    local lines = {
      format('GET %s HTTP/1.1',req.uri or ''),
      format('Host: %s',req.host),
      'Upgrade: websocket',
      'Connection: Upgrade',
      format('Sec-WebSocket-Key: %s',req.key),
      format('Sec-WebSocket-Protocol: %s',table.concat(req.protocols,', ')),
      'Sec-WebSocket-Version: 13',
    }

    if req.origin then
        table.insert(lines,string.format('Origin: %s',req.origin))
    end

    if req.port and req.port ~= 80 then
      lines[2] = format('Host: %s:%d',req.host,req.port)
    end

    table.insert(lines,'\r\n')

    return table.concat(lines,'\r\n')
  end
  
  function accept_upgrade(request, protocols)
    local headers = parse_http_header(request)
    if headers['upgrade'] ~= 'websocket' or not headers['connection'] or 
        not headers['connection']:match('upgrade') or not headers['sec-websocket-key'] or headers['sec-websocket-version'] ~= '13' then
      return nil,'HTTP/1.1 400 Bad Request\r\n\r\n'
    end

    local prot
    if headers['sec-websocket-protocol'] then
      for protocol in headers['sec-websocket-protocol']:gmatch('([^,%s]+)%s?,?') do
        for _,supported in ipairs(protocols) do
          if supported == protocol then
            prot = protocol
            break
          end
        end

        if prot then
          break
        end
      end
    end

    local lines = {
      'HTTP/1.1 101 Switching Protocols',
      'Upgrade: websocket',
      'Connection: '..headers['connection'],
      string.format('Sec-WebSocket-Accept: %s', sec_websocket_accept(headers['sec-websocket-key'])),
    }

    if prot then
        table.insert(lines,string.format('Sec-WebSocket-Protocol: %s',prot))
    end

    table.insert(lines,'\r\n')

    return table.concat(lines,'\r\n'),prot
  end
