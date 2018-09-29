module("tools", package.seeall)

local cid = 1000
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

function genid()
    cid = cid + 1
    return cid
end

function mk_space(num)
    local fmt = string.format("%%%d.%ds", num, num)
    return string.format(fmt, " ")
end

function mk_key(k)
    if type(v) == "string" then
        return string.format('"%s"', k)
    else
        return tostring(k)
    end
end

function dumpDebug(t, step)
    local str = ""
    step = step or 0
    if step == 0 then str = "{\n" end

    if type(t) == "table" then
        for k, v in pairs(t) do
            if type(v) == "table" then
                str = string.format("%s%s[%s] = {\n", str, mk_space(step*2+2), mk_key(k))
                str = string.format("%s%s", str, dumpDebug(v, step+1))
                str = string.format("%s%s}\n", str, mk_space(step*2+2))
            else
                str = string.format("%s%s[%s] = %s\n", str, mk_space(step*2+2), mk_key(k), tostring(v))
            end
        end
    else
        str = string.format("%s%s[%s] = %s\n", str, mk_space(step*2), mk_key(k), tostring(v))
    end

    if step == 0 then
        if str == "{\n" then str = "{ " end
        str = string.format("%s}", str)
    end

    return str
end
