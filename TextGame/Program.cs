using System;
using System.Xml.Linq;

string input;
Item[] inventory = new Item[20];
bool isGame =false;
bool error = false;

for (int i = 0; i < inventory.Length; i++)
{
    inventory[i].name = "0";
}

Console.WriteLine();
Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n");
Console.Write("캐릭터 이름을 정해주세요 : ");

Player player = new Player(Console.ReadLine());

Console.Clear();
Console.WriteLine(player.name + " 캐릭터가 생성되었습니다.\n");
Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");


//inventory[0].equip = true;
//inventory[0].type = "공격";
//inventory[0].name = "무기";
//inventory[0].value = 5;
//inventory[0].gold = 500;
//inventory[0].explain = "실험이요";

if (inventory[0].name == "0")
    inventory[0] = new Item(true, "공격", "무기", 5, 500, "실험이요");



    //여기부터 게임
    while (!isGame)
{
    error = false;

    Console.WriteLine("<스파르타 마을>\n");
    Console.WriteLine("[활동 선택]\n");

    Console.WriteLine("1. 상태 보기");
    Console.WriteLine("2. 인벤토리");
    Console.WriteLine("3. 상점");

    Console.Write("\n원하시는 행동을 입력해 주세요.\n>> ");
    input = Console.ReadLine();

    //상태보기
    if (input == "1")
    {
        Console.Clear();
        Console.WriteLine("1. 상태보기\n");
        Console.WriteLine("[상태창]\n");

        Console.WriteLine("ID : " + player.name);
        Console.WriteLine("Lv : " + player.level.ToString("D2"));
        Console.WriteLine("직업 : " + player.chad);
        Console.WriteLine("공격력 : " + player.attack);
        Console.WriteLine("방어력 : " + player.defensive);
        Console.WriteLine("체력 : " + player.hp);
        Console.WriteLine("GOLD : " + player.gold + "G");

        Console.WriteLine("\n0. 나가기");

        while(!error)
        {
            Console.Write("\n원하시는 행동을 입력해 주세요.\n>> ");
            input = Console.ReadLine();

            if (input == "0")
            {
                Console.Clear();
                Console.WriteLine("나가기 완료\n");
                error = true;
                continue;
            }
            else
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                error = false;
            }
        }
    }
    //인벤토리
    else if (input == "2")
    {
        Console.Clear();
        Console.WriteLine("2. 인벤토리\n");
        Console.WriteLine("[인벤토리]");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

        Console.WriteLine("[아이템 목록]");

        for (int i = 0; i < inventory.Length; ++i)
        {
            if (inventory[i].name != "0")
            {
                Console.Write("- ");
                if (inventory[i].equip == true)
                    Console.Write("[E]");
                Console.WriteLine(inventory[i].name + "    | " + inventory[i].type + "력 +" + inventory[i].value + " | " + inventory[i].explain);

            }
        }

        Console.WriteLine("\n1. 장착 관리");
        Console.WriteLine("0. 나가기");

        while (!error)
        {
            Console.Write("\n원하시는 행동을 입력해 주세요.\n>> ");
            input = Console.ReadLine();

            if (input == "0")
            {
                Console.Clear();
                Console.WriteLine("나가기 완료\n");
                error = true;
                continue;
            }
            else if (input == "1")
            {


            }
            else
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                error = false;
            }
        }
    }
    //상점
    else if (input == "3")
    {
        Console.Clear();
        Console.WriteLine("3. 상점\n");
        Console.WriteLine("[상점]\n");

        Console.WriteLine("[보유 골드]");
        Console.WriteLine(player.gold + " G\n");

        Console.WriteLine("[아이템 목록]");
        Console.WriteLine("- 수련자 갑옷     | 방어력 +5  | 수련에 도움을 주는 갑옷입니다.                   |  1000 G");
        Console.WriteLine("- 무쇠갑옷        | 방어력 +9  | 무쇠로 만들어져 튼튼한 갑옷입니다.               |  1700 G");
        Console.WriteLine("- 스파르타의 갑옷 | 방어력 +15 | 스파르타의 전사들이 사용했다는 전설의 갑옷입니다.|  3500 G");
        Console.WriteLine("- 낡은 검         | 공격력 +2  | 쉽게 볼 수 있는 낡은 검 입니다.                  |   600 G");
        Console.WriteLine("- 청동 도끼       | 공격력 +5  |  어디선가 사용됐던거 같은 도끼입니다.            |  1500 G");
        Console.WriteLine("- 스파르타의 창   | 공격력 +7  | 스파르타의 전사들이 사용했다는 전설의 창입니다.  |  2000 G");


    }
    else
    {
        Console.Clear();
        Console.WriteLine("잘못된 입력입니다.");
    }
}


public struct Player
{
    public Player(string strname)
    {
        name = strname;
        level = 1;
        chad = "초보자";
        attack = 10;
        defensive = 5;
        hp = 100;
        gold = 1500;
    }

    public string name;
    public int level;
    public string chad;
    public int attack;
    public int defensive;   
    public int hp;
    public int gold;
};

public struct Item
{
    public Item()
    {
        equip = false;
        type = "0";
        name = "0";
        value = 0;
        gold = 0;
        explain = "0";
    }

    public Item(bool itemEquip, string itemType, string itemName, int itemValue, int itemGold, string itemExplain )
    {
        equip = itemEquip;
        type = itemType;
        name = itemName;
        value = itemValue;
        gold = itemGold;
        explain = itemExplain;
    }

    public bool equip;
    public string type;
    public string name;
    public int value;
    public int gold;
    public string explain;
};

