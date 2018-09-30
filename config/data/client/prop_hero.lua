--
-- $Id$
--

module( "resmng" )
propHeroLANGKey = {

}

propHeroKey = {
ID = 1, Type = 2, Path = 3, ShapeAsset = 4, NickName = 5, Age = 6, Faith = 7, Hobby = 8, Skin = 9, DescID = 10, Version = 11, 
}

propHeroData = {

	[HERO_CAOPI] = {HERO_CAOPI, ACTOR_HERO, "Hero/Caopi", "ShapeAssetObject/Caopi", 20001, 31001, 33001, 32001, nil, 30001, 1, },
	[HERO_ZHENJI] = {HERO_ZHENJI, ACTOR_HERO, "Hero/Zhenji", nil, nil, nil, nil, nil, nil, nil, nil, },
}



local propHero_mt = {}
propHero_mt.__index = function (_table, _key)
    local lang_idx = propHeroLANGKey[_key]
    if lang_idx then
		local lang_str = propLanguageById(_table[lang_idx])
		local idx_ex = propHeroKey[_key .. "ARG"]
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
    local idx = propHeroKey[_key]
    if not idx then
        return nil
    end
    return _table[idx]
end

function propHeroById(_key_id)
    local id_data = propHeroData[_key_id]
    if id_data == nil then
        return nil
    end
    if getmetatable(id_data) == nil then
        setmetatable(id_data, propHero_mt)
    end
    return id_data
end

