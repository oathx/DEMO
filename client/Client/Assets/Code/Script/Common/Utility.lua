ObjectLayer = {
    UIMODEL = LayerMask.NameToLayer("UIModel"),
    UI      = LayerMask.NameToLayer("UI")
}

function GetFileName(szFilePath)
	local length = string.len(szFilePath)
	
	local sidx = 0
	local eidx = 0
	while true do
		local i, e = string.find(szFilePath, "/", eidx + 1)
		if i == nil or e == nil then
			break
		end
		
		sidx = i
		eidx = e
	end

	return string.sub(szFilePath, eidx + 1, length)
end

function mkSpace(num)
    local fmt = string.format("%%%d.%ds", num, num)
    return string.format(fmt, " ")
end

function dumpTab(t, step)
    local str = ""
    step = step or 0
    if type(t) == "table" then
        for k, v in pairs(t) do
            if type(v) == "table" then
                str = str..string.format("%s%s = {\n", mkSpace(step*2+2), tostring(k))
                str = str..dumpTab(v, step+1)
                str = str..string.format("%s}\n", mkSpace(step*2+2))
            else
                str = str..string.format("%s%s = %s\n", mkSpace(step*2+2), tostring(k), tostring(v))
            end
        end
    else
        str = str..string.format("%s%s = %s\n", mkSpace(step*2), tostring(k), tostring(v))
    end

    return str
end


INFO = function(fmt, ...)
    print(string.format("[INFO](%s) frame=%s %s", 
        Time.time, Time.frameCount, string.format(fmt, ...)))
end

ERROR = function(fmt, ...)
    Debug.LogError(string.format("[ERROR](%s) frame=%s %s %s", 
        Time.time, Time.frameCount, string.format(fmt, ...), debug.traceback()))
end

ELOG = function(fmt, ...)
    Debug.LogError(string.format("[ELOG](%s) frame=%s %s", 
        Time.time, Time.frameCount, string.format(fmt, ...)))
end

