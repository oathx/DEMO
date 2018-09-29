function LOG(str, ...)
    local output = string.format("[DEBUG] %s "..str, os.date("%Y-%m-%d %H:%M:%S"), ...)
    print(output)
end

function INFO(str, ...)
    local output = string.format("[INFO] %s "..str, os.date("%Y-%m-%d %H:%M:%S"), ...)
    print(output)
end

function WARN(str, ...)
    local output = string.format("[WARN] %s "..str, os.date("%Y-%m-%d %H:%M:%S"), ...)
    print(output)
end

function ERROR(str, ...)
    local output = string.format("[ERROR] %s "..str, os.date("%Y-%m-%d %H:%M:%S"), ...)
    print(output)
end

function mkSpace(num)
    local fmt = string.format("%%%d.%ds", num, num)
    return string.format(fmt, " ")
end

function mkKey(k)
    if type(v) == "string" then
        return string.format('"%s"', k)
    else
        return tostring(k)
    end
end

function dumpTab(t, step)
    local str = ""
    step = step or 0
    if step == 0 then str = "{\n" end

    if type(t) == "table" then
        for k, v in pairs(t) do
            if type(v) == "table" then
                str = string.format("%s%s[%s] = {\n", str, mkSpace(step*2+2), mkKey(k))
                str = string.format("%s%s", str, dumpTab(v, step+1))
                str = string.format("%s%s}\n", str, mkSpace(step*2+2))
            else
                str = string.format("%s%s[%s] = %s\n", str, mkSpace(step*2+2), mkKey(k), tostring(v))
            end
        end
    else
        str = string.format("%s%s[%s] = %s\n", str, mkSpace(step*2), mkKey(k), tostring(v))
    end

    if step == 0 then
        if str == "{\n" then str = "{ " end
        str = string.format("%s}", str)
    end

    return str
end

