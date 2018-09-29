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

