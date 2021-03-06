Option Strict On
Imports System
Imports System.Collections

Namespace RDSInterpreter

    Public Enum enLanguage
        Unkown = 0
        Polish = &H20
        Albanian = &H1
        Breton = &H2
        Catalan = &H3
        Croatian = &H4
        Welsh = &H5
        Czech = &H6
        Danish = &H7
        German = &H8
        English = &H9
        Spanish = &HA
        Esperanto = &HB
        Estonian = &HC
        Basque = &HD
        Faroese = &HE
        French = &HF
        Frisian = &H10
        Irish = &H11
        Gaelic = &H12
        Galician = &H13
        Icelandic = &H14
        Italian = &H15
        Lappish = &H16
        Latin = &H17
        Latvian = &H18
        Luxembourgian = &H19
        Lithuanian = &H1A
        Hungarian = &H1B
        Maltese = &H1C
        Dutch = &H1D
        Norwegian = &H1E
        Occitan = &H1F
        Amharic = &H7F
        Arabic = &H7E
        Armenian = &H7D
        Assamese = &H7C
        Azerbijani = &H7B
        Bambora = &H7A
        Belorussian = &H79
        Bengali = &H78
        Bulgarian = &H77
        Burmese = &H76
        Chinese = &H75
        Churash = &H74
        Dari = &H73
        Fulani = &H72
        Georgian = &H71
        Greek = &H70
        Gujurati = &H6F
        Gurani = &H6E
        Hausa = &H6D
        Hebrew = &H6C
        Hindi = &H6B
        Indonesian = &H6A
        Japanese = &H69
        Kannada = &H68
        Kazakh = &H67
        Khmer = &H66
        Korean = &H65
        Laotian = &H64
        Macedonian = &H63
        Malagasay = &H62
        Malaysian = &H61
        Moldavian = &H60
        Portuguese = &H21
        Romanian = &H22
        Romansh = &H23
        Serbian = &H24
        Slovak = &H25
        Slovene = &H26
        Finnish = &H27
        Swedish = &H28
        Turkish = &H29
        Flemish = &H2A
        Walloon = &H2B
        Reserved45 = &H2C
        Reserved46 = &H2D
        Reserved47 = &H2E
        Reserved48 = &H2F
        Reserved49 = &H30
        Reserved50 = &H31
        Reserved51 = &H32
        Reserved52 = &H33
        Reserved53 = &H34
        Reserved54 = &H35
        Reserved55 = &H36
        Reserved56 = &H37
        Reserved57 = &H38
        Reserved58 = &H39
        Reserved59 = &H3A
        Reserved60 = &H3B
        Reserved61 = &H3C
        Reserved62 = &H3D
        Reserved63 = &H3E
        Marathi = &H5F
        Ndebele = &H5E
        Nepali = &H5D
        Oriya = &H5C
        Papamiento = &H5B
        Persian = &H5A
        Punjabi = &H59
        Pushtu = &H58
        Quechua = &H57
        Russian = &H56
        Ruthenian = &H55
        SerboCroat = &H54
        Shona = &H53
        Sinhalese = &H52
        Somali = &H51
        SrananTongo = &H50
        Swahili = &H4F
        Tadzhik = &H4E
        Tamil = &H4D
        Tatar = &H4C
        Telugu = &H4B
        Thai = &H4A
        Ukrainian = &H49
        Urdu = &H48
        Uzbek = &H47
        Vietnamese = &H46
        Zulu = &H45
        Reserved = &H44
        Reserved67 = &H43
        Reserved66 = &H42
        Reserved65 = &H41
        Background = &H40
    End Enum


    Public Enum enMS
        Music = 1
        Speech = 0
        Undefined = 255
    End Enum

    Public Enum enPTY
        NoPTY = 0
        News = 1
        CurrentAffairs = 2
        Information = 3
        Sport = 4
        Education = 5
        Drama = 6
        Culture = 7
        Science = 8
        Varied = 9
        PopMusic = 10
        RockMusic = 11
        EasyListeningMusic = 12
        LightClassical = 13
        SeriousClassical = 14
        OtherMusic = 15
        Weather = 16
        Finance = 17
        ChildrensPrograms = 18
        SocialAffairs = 19
        Religion = 20
        PhoneIn = 21
        Travel = 22
        Leisure = 23
        Jazz = 24
        Country = 25
        NationalMusic = 26
        Oldies = 27
        Folk = 28
        Documentary = 29
        AlarmTest = 30
        Alarm = 31
        'RBDS extras starts here
        Talk
        Top40
        Soft
        ClassicRock
        AdultHits
        SoftRock
        Nostalgica
        Classic
        RhytmBlues
        SoftRhytmBlues
        ForeignLanguage
        ReligiousMusic
        ReligiousTalk
        Personality
        Public_
        College
        Unassigned
    End Enum
    Public Enum enCountryCodeMapExt As Short
        Undefined = 0
        Germany = &HE01
        Algeria = &HE02
        Andorra = &HE03
        Israel = &HE04
        Italy = &HE05
        Belgium = &HE06
        RussianFederation = &HE07
        Palestine = &HE08
        Albania = &HE09
        Austria = &H10
        Hungary = &HE0B
        Malta = &HE0C
        Germany_2 = &HE0D
        Egypt = &HE0F
        Greece = &HE11
        Cyprus = &HE12
        SanMarino = &HE13
        Switzerland = &HE14
        Jordan = &HE15
        Finland = &HE16
        Luxembourg = &HE17
        Bulgaria = &HE18
        Denmark = &HE19
        Gibraltar = &HE1A
        Iraq = &HE1B
        UnitedKingdom = &HE1C
        Libya = &HE1D
        Romania = &HE1E
        France = &HE1F
        Morocco = &HE21
        CzechRepublic = &HE22
        Poland = &HE23
        VaticanCityState = &HE24
        Slovakia = &HE25
        SyrianArabRepublic = &HE26
        Tunisia = &HE27
        Liechtenstein = &HE29
        Iceland = &HE2A
        Monaco = &HE2B
        Lithuania = &HE2C
        Yugoslavia = &HE2D
        Spain = &HE2E
        Norway = &HE2F
        Ireland = &HE32
        Turkey = &HE33
        Macedonia = &HE34
        Netherlands = &HE38
        Latvia = &HE39
        Lebanon = &HE3A
        Croatia = &HE3C
        Sweden = &HE3E
        Belarus = &HE3F
        Moldova = &HE41
        Estonia = &HE42
        Ukraine = &HE46
        Portugal = &HE48
        Slovenia = &HE49
        BosniaHerzegovin = &H4F

        AscensionIsland = &HD1A
        Cabinda = &HD34
        Angola = &HD06
        'Algeria = &HE02
        Burundi = &HD19
        Benin = &HD0E
        BurkinaFaso = &HD0B
        Botswana = &HD1B
        Cameroon = &HD01
        CanaryIslands = &HE0E
        CentralAfricanRepublic = &HD02
        Chad = &HD29
        Congo = &HD0C
        Comoros = &HD1C
        CapeVerde = &HD16
        CotedIvoire = &HD2C
        DemocraticRepublicofCongo = &HD2B
        Djibouti = &HD03
        'Egypt = &HE0F
        Ethiopia = &HD1E
        Gabon = &HD08
        Ghana = &HD13
        Gambia = &HD18
        GuineaBissau = &HD2A
        EquatorialGuinea = &HD07
        RepublicofGuinea = &HD09
        KenyaKE = &HD26
        Liberia = &HD12
        'Libya = &HE1D
        Lesotho = &HD36
        Maurituis = &HD3A
        Madagascar = &HD04
        Mali = &HD05
        Mozambique = &HD23
        'Morocco = &HE21
        Mauritania = &HD14
        Malawi = &HD0F
        Niger = &HD28
        Nigeria = &HD1F
        Namibia = &HD11
        Rwanda = &HD35
        SaoTomePrincipe = &HD15
        Sechelles = &HD38
        Senegal = &HD17
        SierraLeone = &HD21
        Somalia = &HD27
        SouthAfrica = &HD0A
        Sudan = &HD3C
        Swaziland = &HD25
        Togo = &HD0D
        'Tunisia = &HE27
        Tanzania = &HD1D
        Uganda = &HD24
        WesternSahara = &HD33
        Zambia = &HD2E
        Zanzibar = &HD2D
        Zimbabwe = &HD22
        Armenia = &HE4A
        Azerbaijan = &HE3B
        'Belarus = &HE3F
        'Estonia = &HE42
        Georgia = &HE4C
        Kazakhstan = &HE3D
        Kyrghyzstan = &HE43
        'Latvia = &HE39
        Lithunia = &HE2C
        'Moldova = &HE41
        'RussianFederation = &HE07
        Tajikistan = &HE35
        Turkmenistan = &HE4E
        'Ukraine = &HE46
        Uzbekistan = &HE4B
        Anguilla = &HA21
        AntiguaandBarbuda = &HA22
        Argentina = &HA2A
        Aruba = &HA43
        Bahamas = &HA2F
        Barbados = &HA25
        Belize = &HA26
        Bermuda = &HA2C
        Bolivia = &HA31
        Brazil = &HA2B
        Canada1 = &HA1B
        Canada2 = &HA1C
        Canada3 = &HA1D
        Canada4 = &HA1E
        CaymanIslands = &HA27
        Chile = &HA3C
        Colombia = &HA32
        CostaRica = &HA28
        Cuba = &HA29
        Dominica = &HA3A
        DominicanRepublic = &HA3B
        Ecuador = &HA23
        ElSalvador = &HA4C
        FalklandIslands = &HA24
        Greenland = &HA1F
        Grenada = &HA3D
        Guadeloupe = &HA2E
        Guatemala = &HA41
        Guiana = &HA35
        Guyana = &HA3F
        Haiti = &HA4D
        Honduras = &HA42
        Jamaica = &HA33
        Martinique = &HA34
        Mexico1 = &HA5B
        Mexico2 = &HA5E
        Mexico3 = &HA5F
        Mexico4 = &HA5D
        Montserrat = &HA45
        NetherlandsAntilles = &HA2D
        Nicaragua = &HA37
        Panama = &HA39
        Paraguay = &HA36
        Peru = &HA47
        'PuertoRico
        USA1 = &HA01
        USA2 = &HA02
        USA3 = &HA03
        USA4 = &HA04
        USA5 = &HA05
        USA6 = &HA06
        USA7 = &HA07
        USA8 = &HA08
        USA9 = &HA09
        USA10 = &HA0A
        USA11 = &HA0B
        USA12 = &HA0D
        USA13 = &HA0E

        SaintKitts = &HA4A
        SaintLucia = &HA4B
        StPierreandMiquelon = &HA6F
        SaintVincent = &HA5C
        Suriname = &HA48
        TrinidadandTobago = &HA46
        TurksandCaicosIslands = &HA3E


        Uruguay = &HA49
        Venezuela = &HA4E
        VirginIslandsBritish = &HA5F
        'VirginIslands[USA] =&hA,1..9,B,D,EA0
        Afghanistan = &HF0A
        SaudiArabia = &HF09
        AustraliaCapitalTerritory = &HF01
        NewSouthWales = &HF02
        Victoria = &HF03
        Queensland = &HF04
        SouthAustralia = &HF05
        WesternAustralia = &HF06
        Tasmania = &HF07
        NorthernTerritory = &HF08
        Bangladesh = &HF13
        Bahrain = &HF0E
        MyanmarBurma = &HF0B
        BruneiDarussalam = &HF1B
        Bhutan = &HF12
        Cambodia = &HF23
        China = &HF0C
        SriLanka = &HF1C
        Fiji = &HF15
        HongKong = &HF1F
        India = &HF25
        Indonesia = &HF2C
        Iran = &HF18
        'Iraq = &HE1B
        Japan = &HF29
        Kiribati = &HF11
        KoreaSouth = &HF1E
        KoreaNorth = &HF0D
        Kuwait = &HF21
        Laos = &HF31
        Macau = &HF26
        Malaysia = &HF0F
        Maldives = &HF2B
        Micronesia = &HF3E
        Mongolia = &HF3F
        Nepal = &HF2E
        Nauru = &HF17
        NewZealand = &HF19
        Oman = &HF16
        Pakistan = &HF14
        Philippines = &HF28
        PapuaNewGuinea = &HF39
        Qatar = &HF22
        SolomanIslands = &HF1A
        WesternSamoa = &HF24
        Singapore = &HF2A
        Taiwan = &HF1D
        Thailand = &HF32
        Tonga = &HF33
        UAE = &HF2D
        Vietnam = &HF27
        Vanuatu = &HF2F
        Yemen = &HF3B
    End Enum

    Public Enum enCoverageAreaCode
        Local = 0
        International = 1
        National = 2
        SupraRegional = 3
        Regional1 = 4
        Regional2 = 5
        Regional3 = 6
        Regional4 = 7
        Regional5 = 8
        Regional6 = 9
        Regional7 = 10
        Regional8 = 11
        Regional9 = 12
        Regional10 = 13
        Regional11 = 14
        Regional12 = 15
        Undefined = 255
    End Enum

    Public Enum enAFMethod
        A = 0
        B = 1
        UNKNOWN = 2
    End Enum

    Public Enum enDICode
        Mono_NotArtificialHead_NotCompressed_StaticPTY = 0
        Stereo_NotArtificialHead_NotCompressed_StaticPTY = 1
        Mono_ArtificialHead_NotCompressed_StaticPTY = 2
        Stereo_ArtificialHead_NotCompressed_StaticPTY = 3
        Mono_NotArtificialHead_Compressed_StaticPTY = 4
        Stereo_NotArtificialHead_Compressed_StaticPTY = 5
        Mono_ArtificialHead_Compressed_StaticPTY = 6
        Stereo_ArtificialHead_Compressed_StaticPTY = 7
        Mono_NotArtificialHead_NotCompressed_DynamicPTY = 8
        Stereo_NotArtificialHead_NotCompressed_DynamicPTY = 9
        Mono_ArtificialHead_NotCompressed_DynamicPTY = 10
        Stereo_ArtificialHead_NotCompressed_DynamicPTY = 11
        Mono_NotArtificialHead_Compressed_DynamicPTY = 12
        Stereo_NotArtificialHead_Compressed_DynamicPTY = 13
        Mono_ArtificialHead_Compressed_DynamicPTY = 14
        Stereo_ArtificialHead_Compressed_DynamicPTY = 15
        Undefined = 255
    End Enum
    Public Enum enGroupVersionMap
        A = 0
        B = 1
    End Enum


    Public Enum enBlockType
        A = 0
        B = 1
        C = 2
        D = 3
        C2 = 4
        E = 5
        iE = 6
        ib = 7
    End Enum
    Public Enum engroupTypeMap
        group0A = 0
        group0B = 1
        group1A = 2
        group1B = 3
        group2A = 4
        group2B = 5
        group3A = 6
        group3B = 7
        group4A = 8
        group4B = 9
        group5A = 10
        group5B = 11
        group6A = 12
        group6B = 13
        group7A = 14
        group7B = 15
        group8A = 16
        group8B = 17
        group9A = 18
        group9B = 19
        group10A = 20
        group10B = 21
        group11A = 22
        group11B = 23
        group12A = 24
        group12B = 25
        group13A = 26
        group13B = 27
        group14A = 28
        group14B = 29
        group15A = 30
        group15B = 31
        NotSet = 32 'needed for TMCGroup
    End Enum

    Namespace TMC
        Public Enum enGrouptype
            TMC_GROUP = 0
            TMC_SINGLE = 1
            TMC_SYSTEM = 2
            TMC_TUNING = 3
        End Enum
        Public Enum Duration
            NOTSET = -1
            no_explicit_duration = 0
            _15min = 1
            _30min = 2
            _1h = 3
            _2h = 4
            _3h = 5
            _4h = 6
            rest_of_the_day = 7
        End Enum
    End Namespace

End Namespace
