namespace RpgBuilderMvc.GameData;

public static class ItemsData
{
    public static readonly List<Armor> Armors =
    [   
        //Armaduras Leves
        new Armor(1, "Acolchoada", "5 po", "11 + modificador de Destreza", "", "Desvantagem", "4 kg"),
        new Armor(2, "Couro", "10 po", "11 + modificador de Destreza", "", "", "5 kg"),
        new Armor(3, "Couro Batido", "45 po", "12 + modificador de Destreza", "", "", "6,5 kg"),

        //Armaduras Médias
        new Armor(4, "Gibão de Peles", "10 po", "12 + modificador de Destreza (máx. +2)", "", "", "6 kg"),
        new Armor(5, "Camisão de Malha", "30 po", "13 + modificador de Destreza (máx. +2)", "", "", "10 kg"),
        new Armor(6, "Brunea", "50 po", "14 + modificador de Destreza (máx. +2)", "", "Desvantagem", "22,5 kg"),
        new Armor(7, "Peitoral", "400 po", "14 + modificador de Destreza (máx. +2)", "", "", "10 kg"),
        new Armor(8, "Meia-Armadura", "750 po", "15 + modificador de Destreza", "", "Desvantagem", "20 kg"),

        //Armaduras Pesadas
        new Armor(9, "Cota de anéis", "30 po", "14", "", "Desvantagem", "20 kg"),
        new Armor(10, "Cota de Malha", "75 po", "16", "For 13", "Desvantagem", "27,5 kg"),
        new Armor(11, "Acolchoada", "5 po", "11 + modificador de Destreza", "", "Desvantagem", "4 kg"),
        new Armor(12, "Acolchoada", "5 po", "11 + modificador de Destreza", "", "Desvantagem", "4 kg"),
        new Armor(13, "Acolchoada", "5 po", "11 + modificador de Destreza", "", "Desvantagem", "4 kg")
    ];
    public static readonly List<Weapon> Weapon =
    [
        //Armas Simples Corpo-a-Corpo
        new Weapon(1, "Adaga", "2 po", "1d4 perfurante", "0,5 kg", "Acuidade, leve, arremesso (distância 6/18)"),
        new Weapon(2, "Azagaia", "5 pp", "1d6 perfurante", "1 kg", "Arremesso (distância 9/36)"),
        new Weapon(3, "Bordão", "2 pp", "1d6 concussão", "2 kg", "Versátil (1d8)"),
        new Weapon(4, "Clava Grande", "2 pp", "1d8 concussão", "5 kg", "Pesada, duas mãos"),
        new Weapon(5, "Foice Curta", "1 po", "1d4 cortante", "1 kg", "Leve"),
        new Weapon(6, "Lança", "1 po", "1d6 perfurante", "1,5 kg", "Arremesso (distância 6/18), versátil (1d8)"),
        new Weapon(7, "Maça", "5 po", "1d6 concussão", "2 kg", ""),
        new Weapon(8, "Machadinha", "5 po", "1d6 cortante", "1 kg", "Leve, arremesso (distância 6/18)"),
        new Weapon(9, "Martelo Leve", "2 po", "1d4 concussão", "1 kg", "Leve, arremesso (distância 6/18)"),
        new Weapon(10, "Porrete", "1 pp", "1d4 concussão", "1 kg", "Leve"),

        //Armas Simples à Distância
        new Weapon(11, "Arco Curto", "25 po", "1d6 perfurante", "1 kg", "Munição (distância 24/96), duas mãos"),
        new Weapon(12, "Beste Leve", "25 po", "1d8 perfurante", "2,5 kg", "Munição (distância 24/96), recarga, duas mãos"),
        new Weapon(13, "Dardo", "5 pc", "1d4 perfurante", "0,1 kg", "Acuidade, arremesso (distância 6/18)"),
        new Weapon(14, "Funda", "1 pp", "1d4 concussão", "kg", "Munição (distância 9/36)"),

        //Armas Marciais Corpo-a-Corpo
        new Weapon(15, "Alabarda", "20 po", "1d10 cortante", "3 kg", "Pesada, alcance, duas mãos"),
        new Weapon(16, "Cimitarra", "25 po", "1d6 cortante", "1,5 kg", "Acuidade, leve"),
        new Weapon(17, "Chicote", "2 po", "1d4 cortante", "1,5 kg", "Acuidade, alcance"),
        new Weapon(18, "Espada Curta", "10 po", "1d6 perfurante", "1 kg", "Acuidade, leve"),
        new Weapon(19, "Espada Grande", "50 po", "2d6 cortante", "3 kg", "Pesada, duas mãos"),
        new Weapon(20, "Espada Longa", "15 po", "1d8 cortante", "1,5 kg", "Versátil (1d10)"),
        new Weapon(21, "Glaive", "20 po", "1d10 cortante", "3 kg", "Pesada, alcance, duas mãos"),
        new Weapon(22, "Lança de Montaria", "10 po", "1d12 perfurante", "3 kg", "Alcance, especial"),
        new Weapon(23, "Lança Longa", "5 po", "1d10 perfurante", "4 kg", "Pesada, alcance, duas mãos"),
        new Weapon(24, "Maça Estrela", "15 po", "1d8 perfurante", "2 kg", ""),
        new Weapon(25, "Machado Grande", "30 po", "1d12 cortante", "3,5 kg", "Pesada, duas mãos"),
        new Weapon(26, "Machado de Batalha", "10 po", "1d8 cortante", "2 kg", "Versátil (1d10)"),
        new Weapon(27, "Malho", "10 po", "2d6 concussão", "5 kg", "Pesada, duas mãos"),
        new Weapon(28, "Mangual", "10 po", "1d8 concussão", "1 kg", ""),
        new Weapon(29, "Martelo de Guerra", "15 po", "1d8 concussão", "1 kg", "Versátil (1d10)"),
        new Weapon(30, "Picareta de Guerra", "5 po", "1d8 perfurante", "1 kg", ""),
        new Weapon(31, "Rapieira", "25 po", "1d8 perfurante", "1 kg", "Acuidade"),
        new Weapon(32, "Tridente", "5 po", "1d6 perfurante", "2 kg", "Arremesso (6/18), versátil (1d8)"),

        //Armas Marciais à Distância
        new Weapon(33, "Arco Longo", "50 po", "1d8 perfurante", "1 kg", "Munição (distância 45/180), pesada, duas mãos"),
        new Weapon(34, "Besta de Mão", "75 po", "1d6 perfurante", "1,5 kg", "Munição (distância 9/36), leve, recarga"),
        new Weapon(35, "Besta Pesada", "50 po", "1d10 perfurante", "4,5 kg", "Munição (distância 30/120), pesada, recarga, duas mãos"),
        new Weapon(36, "Rede", "1 po", "", "1,5 kg", "Especial, arremesso (distância 1,5/4,5)"),
        new Weapon(37, "Zarabatana", "10 po", "1 perfurante", "0,5 kg", "Munição (distância 7,5/30), recarga")
    ];
}

public record Armor(int Id, string Name, string Price, string Ac, string Strength, string Stealth, string Weight);
public record Weapon(int Id, string Name, string Price, string Damage, string Weight, string Properties);