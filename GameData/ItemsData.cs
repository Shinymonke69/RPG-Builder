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

}

public record Armor(int Id, string Nome, string Preço, string Ca, string Força, string Furtividade, string Peso);
