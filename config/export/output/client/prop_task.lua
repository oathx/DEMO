--
-- $Id$
--

module( "resmng" )
propTaskLANGKey = {

}

propTaskKey = {
ID = 1, Path = 2, Type = 3, Scene = 4, GenerateMonster = 5, Script = 6, 
}

propTaskData = {

	[TASK_TEST] = {TASK_TEST, 10001, 0, 1, nil, "", },
}



local propTask_mt = {}
propTask_mt.__index = function (_table, _key)
    local lang_idx = propTaskLANGKey[_key]
    if lang_idx then
		local lang_str = propLanguageById(_table[lang_idx])
		local idx_ex = propTaskKey[_key .. "ARG"]
		local lang_args = _table[idx_ex]
		if lang_args then
			if #lang_args > 0 then
				for k, v in ipairs(lang_args) do
					lang_args[k] = parse_language_id_arg(v)
				end
				return string.format(lang_str,unpack(lang_args))
			end
		end
		return lang_str
    end
    local idx = propTaskKey[_key]
    if not idx then
        return nil
    end
    return _table[idx]
end

function propTaskById(_key_id)
    local id_data = propTaskData[_key_id]
    if id_data == nil then
        return nil
    end
    if getmetatable(id_data) == nil then
        setmetatable(id_data, propTask_mt)
    end
    return id_data
end

