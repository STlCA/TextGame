using System;
using System.Xml.Linq;

string input;
string itemNum;
Item[] inventory = new Item[20];
Item[] shop = new Item[7];
bool isGame =false;
bool error = false;
bool error2 = false;
int itemCount = 0;
//string result;

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

//실험용
//if (inventory[0].name == "0")
//    inventory[0] = new Item(false, "공격", "무기", 5, 500, "실험이요");

//bool itemEquip, string itemType, string itemName, int itemValue, int itemGold, string itemExplain 
shop[1] = new Item(false, "방어", "수련자 갑옷", 5, 1000, "수련에 도움을 주는 갑옷입니다.");
shop[2] = new Item(false, "방어", "무쇠 갑옷", 9, 1700, "무쇠로 만들어져 튼튼한 갑옷입니다.");
shop[3] = new Item(false, "방어", "스파르타의 갑옷", 15, 3500, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.");
shop[4] = new Item(false, "공격", "낡은 검", 2, 600, "쉽게 볼 수 있는 낡은 검 입니다.");
shop[5] = new Item(false, "공격", "청동 도끼", 5, 1500, "어디선가 사용됐던거 같은 도끼입니다.");
shop[6] = new Item(false, "공격", "스파르타의 창", 7, 2000, "스파르타의 전사들이 사용했다는 전설의 창입니다.");

//여기부터 게임
while (!isGame)
{
    error = false;

    Console.WriteLine("<스파르타 마을>\n");
    Console.WriteLine("[활동 선택]\n");

    Console.WriteLine("1. 상태 보기");
    Console.WriteLine("2. 인벤토리");
    Console.WriteLine("3. 상점");
    Console.WriteLine("\n5. 게임 끝내기");

    Console.Write("\n원하시는 행동을 입력해 주세요.\n>> ");
    input = Console.ReadLine();

    //상태보기
    if (input == "1")
    {
        Console.Clear();
        Console.WriteLine("선택 : 1. 상태보기\n");

        while (!error)
        {
            Console.WriteLine("[상태창]\n");

            Console.WriteLine("ID : " + player.name);
            Console.WriteLine("Lv : " + player.level.ToString("D2"));
            Console.WriteLine("직업 : " + player.chad);

            if (player.plusAttack != 0)
                Console.WriteLine("공격력 : " + (player.attack + player.plusAttack) + " (+{0})", player.plusAttack);
            else
                Console.WriteLine("공격력 : " + player.attack);

            if (player.plusDefensive != 0)
                Console.WriteLine("방어력 : " + (player.defensive + player.plusDefensive) + " (+{0})", player.plusDefensive);
            else
                Console.WriteLine("방어력 : " + player.defensive);

            Console.WriteLine("체력 : " + player.hp);
            Console.WriteLine("GOLD : " + player.gold + "G");

            Console.WriteLine("\n0. 나가기");

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
                Console.Clear();
                Console.WriteLine("잘못된 입력입니다.");                
            }
        }
    }
    //인벤토리
    else if (input == "2")
    {
        Console.Clear();
        Console.WriteLine("선택 : 2. 인벤토리\n");

        while (!error)
        {
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
                Console.Clear();
                Console.WriteLine("선택 : 1. 장착 관리\n");

                error2 = false;
                while (!error2)
                {
                    Console.WriteLine("[인벤토리] - 장착관리");
                    Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                    Console.WriteLine("[아이템 목록]");

                    itemCount = 0;
                    for (int i = 0; i < inventory.Length; ++i)
                    {
                        if (inventory[i].name != "0")
                        {
                            Console.Write("- " + (i + 1) + " ");
                            if (inventory[i].equip == true)
                                Console.Write("[E]");
                            Console.WriteLine(inventory[i].name + "    | " + inventory[i].type + "력 +" + inventory[i].value + " | " + inventory[i].explain);
                            itemCount++;
                        }
                    }

                    Console.WriteLine("\n장비번호 선택");
                    Console.WriteLine("0. 나가기");

                    Console.Write("\n원하시는 행동을 입력해 주세요.\n>> ");
                    input = Console.ReadLine();

                    int temp;
                    bool isSuccess;
                    isSuccess = int.TryParse(input, out temp);

                    if (isSuccess == true && int.Parse(input) > 0 && int.Parse(input) <= itemCount)
                    {
                        if (inventory[(int.Parse(input)) - 1].equip == false)
                        {
                            inventory[(int.Parse(input)) - 1].equip = true;
                            Console.Clear();
                            Console.WriteLine(input + "번 장비 선택. 장비를 장착합니다.\n");

                            if (inventory[(int.Parse(input)) - 1].type == "공격")
                                player.plusAttack += inventory[(int.Parse(input)) - 1].value;
                            else if (inventory[(int.Parse(input)) - 1].type == "방어")
                                player.plusDefensive += inventory[(int.Parse(input)) - 1].value;

                        }
                        else
                        {
                            inventory[(int.Parse(input)) - 1].equip = false;
                            Console.Clear();
                            Console.WriteLine(input + "번 장비 선택. 장비를 해제합니다.\n");

                            if (inventory[(int.Parse(input)) - 1].type == "공격")
                                player.plusAttack -= inventory[(int.Parse(input)) - 1].value;
                            else if (inventory[(int.Parse(input)) - 1].type == "방어")
                                player.plusDefensive -= inventory[(int.Parse(input)) - 1].value;
                        }
                    }
                    else if (input == "0")
                    {
                        Console.Clear();
                        Console.WriteLine("나가기 완료\n");
                        error2 = true;
                        error = true;//한번에 마을나갈때
                        continue;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.\n");
                    }
                }  
            }
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 입력입니다.\n");                
            }
        }
    }
    //상점
    else if (input == "3")
    {
        Console.Clear();
        itemCount = 0;
        for(int i = 0; i < shop.Length; ++i)
        {
            if (shop[i].name != "0")
            {
                itemCount++;
            }
        }

        Console.WriteLine("3. 상점\n");


        error = false;
        while (!error)
        {
            Console.WriteLine("[상점]");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine(player.gold + " G\n");

            Console.WriteLine("[아이템 목록]");
            //result = (shop[1].state == true) ? shop[1].gold.ToString() + " G" : "구매완료";
            Console.WriteLine("- {0}     | {1}력 +{2}  | {3}                   |  {4}", shop[1].name, shop[1].type, shop[1].value, shop[1].explain, 
                ((shop[1].state == true) ? shop[1].gold.ToString() + " G" : "구매완료"));
            Console.WriteLine("- {0}       | {1}력 +{2}  | {3}               |  {4}", shop[2].name, shop[2].type, shop[2].value, shop[2].explain,
                ((shop[2].state == true) ? shop[2].gold.ToString() + " G" : "구매완료"));
            Console.WriteLine("- {0} | {1}력 +{2} | {3}|  {4}", shop[3].name, shop[3].type, shop[3].value, shop[3].explain,
                ((shop[3].state == true) ? shop[3].gold.ToString() + " G" : "구매완료"));
            Console.WriteLine("- {0}         | {1}력 +{2}  | {3}                  |   {4}", shop[4].name, shop[4].type, shop[4].value, shop[4].explain,
                ((shop[4].state == true) ? shop[4].gold.ToString() + " G" : "구매완료"));
            Console.WriteLine("- {0}       | {1}력 +{2}  | {3}             |  {4}", shop[5].name, shop[5].type, shop[5].value, shop[5].explain,
                ((shop[5].state == true) ? shop[5].gold.ToString() + " G" : "구매완료"));
            Console.WriteLine("- {0}   | {1}력 +{2}  | {3}  |  {4}", shop[6].name, shop[6].type, shop[6].value, shop[6].explain,
                ((shop[6].state == true) ? shop[6].gold.ToString() + " G" : "구매완료"));

            Console.WriteLine("\n1. 아이템 구매");
            Console.WriteLine("0. 나가기");

            Console.Write("\n원하시는 행동을 입력해 주세요.\n>> ");
            input = Console.ReadLine();
            
            if (input == "1")
            {
                Console.Clear();
                Console.WriteLine("선택 : 1. 아이템 구매\n");

                error2 = false;
                while (!error2)
                {
                    Console.WriteLine("[상점] - 아이템 구매");
                    Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                    Console.WriteLine("[보유 골드]");
                    Console.WriteLine(player.gold + " G\n");

                    Console.WriteLine("[아이템 목록]");
                    Console.WriteLine("- 1 {0}     | {1}력 +{2}  | {3}                   |  {4}", shop[1].name, shop[1].type, shop[1].value, shop[1].explain,
                        ((shop[1].state == true) ? shop[1].gold.ToString() + " G" : "구매완료"));
                    Console.WriteLine("- 2 {0}       | {1}력 +{2}  | {3}               |  {4}", shop[2].name, shop[2].type, shop[2].value, shop[2].explain,
                        ((shop[2].state == true) ? shop[2].gold.ToString() + " G" : "구매완료"));
                    Console.WriteLine("- 3 {0} | {1}력 +{2} | {3}|  {4}", shop[3].name, shop[3].type, shop[3].value, shop[3].explain,
                        ((shop[3].state == true) ? shop[3].gold.ToString() + " G" : "구매완료"));
                    Console.WriteLine("- 4 {0}         | {1}력 +{2}  | {3}                  |   {4}", shop[4].name, shop[4].type, shop[4].value, shop[4].explain,
                        ((shop[4].state == true) ? shop[4].gold.ToString() + " G" : "구매완료"));
                    Console.WriteLine("- 5 {0}       | {1}력 +{2}  | {3}             |  {4}", shop[5].name, shop[5].type, shop[5].value, shop[5].explain,
                        ((shop[5].state == true) ? shop[5].gold.ToString() + " G" : "구매완료"));
                    Console.WriteLine("- 6 {0}   | {1}력 +{2}  | {3}  |  {4}", shop[6].name, shop[6].type, shop[6].value, shop[6].explain,
                        ((shop[6].state == true) ? shop[6].gold.ToString() + " G" : "구매완료"));

                    Console.WriteLine("\n구매할 아이템 번호 선택");
                    Console.WriteLine("0. 나가기");

                    Console.Write("\n원하시는 행동을 입력해 주세요.\n>> ");
                    itemNum = Console.ReadLine();

                    int temp;
                    bool isSuccess;
                    isSuccess = int.TryParse(itemNum, out temp);

                    if (isSuccess == true && int.Parse(itemNum) > 0 && int.Parse(itemNum) < itemCount)
                    {
                        Console.WriteLine("\n구매하시겠습니까?");
                        Console.WriteLine("1. 네  | 2. 아니오");

                        Console.Write("\n원하시는 행동을 입력해 주세요.\n>> ");
                        input = Console.ReadLine();

                        if (input == "1")
                        {
                            if (shop[int.Parse(itemNum)].state == false)
                            {
                                Console.Clear();
                                Console.WriteLine("이미 구매한 아이템입니다.\n");
                            }

                            else if (shop[int.Parse(itemNum)].gold <= player.gold)
                            {
                                shop[int.Parse(itemNum)].state = false;
                                player.gold -= shop[int.Parse(itemNum)].gold;

                                itemCount = 0;
                                for (int i = 0; i < inventory.Length; ++i)
                                {
                                    if (inventory[i].name != "0")
                                        itemCount++;
                                }
                                inventory[itemCount] = shop[int.Parse(itemNum)];

                                Console.Clear();
                                Console.WriteLine("구매 완료!\n");
                                error2 = true;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("골드가 모자랍니다.\n");                            
                            }
                        }
                        else if (input == "2")
                        {
                            Console.Clear();
                            Console.WriteLine("선택 : 2. 아니오\n");
                            error2 = true;
                            continue;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("잘못된 입력입니다.\n");
                        }
                        
                        
                    }
                    else if (itemNum == "0")
                    {
                        Console.Clear();
                        Console.WriteLine("나가기 완료\n");
                        error2 = true;
                        error = true;//한번에마을로나가기
                        continue;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다.\n");
                    }
                }
            }
            else if (input == "0")
            {
                Console.Clear();
                Console.WriteLine("나가기 완료\n");
                error = true;
                continue;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 입력입니다.\n");                
            }

        }
    }
    else if (input == "5")
    {
        Console.WriteLine("\n게임을 종료합니다.");
        return;
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
        hp = 100;
        gold = 1500;
        plusAttack = 0;
        attack = 10;
        plusDefensive = 0;
        defensive = 5;
    }

    public string name;
    public int level;
    public string chad;
    public int hp;
    public int gold;
    public int plusAttack;
    public int attack;
    public int plusDefensive;
    public int defensive;   
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
        state = true;
    }

    public Item(bool itemEquip, string itemType, string itemName, int itemValue, int itemGold, string itemExplain )
    {
        equip = itemEquip;
        type = itemType;
        name = itemName;
        value = itemValue;
        gold = itemGold;
        explain = itemExplain;
        state = true;
    }

    public bool equip;
    public string type;
    public string name;
    public int value;
    public int gold;
    public string explain;
    public bool state;
};

