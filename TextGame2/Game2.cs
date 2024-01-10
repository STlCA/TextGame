
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace TextGame2
{
    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
    }

    public class Item
    {
        public string Name { get; }
        public string Description { get; }
        public int Type { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }

        public bool IsEquipped { get; set; }
        public static int ItemCnt = 0;
        public Item(string name, string description, int type, int atk, int def, int hp, bool isEquipped = false)
        {
            Name = name;
            Description = description;
            Type = type;
            Atk = atk;
            Def = def;
            Hp = hp;
            IsEquipped = isEquipped;
        }
        
        public void PrintItemStatDescription(bool withNumber = false, int idx = 0)
        {
            Console.Write("- ");
            if(withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("{0} ", idx);
                Console.ResetColor();
            }
            if (IsEquipped)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("E");
                Console.ResetColor();
                Console.Write("]");
                Console.Write(PadRightForMixedText(Name, 9));
            }
            else
                Console.Write(PadRightForMixedText(Name, 12));
            Console.Write(" | ");

            if (Atk != 0) Console.Write($"Atk {(Atk > 0 ? "+" : "")}{Atk} ");
            if (Def != 0) Console.Write($"Def {(Def > 0 ? "+" : "")}{Def} ");
            if (Hp != 0) Console.Write($"Atk {(Hp > 0 ? "+" : "")}{Hp} ");

            Console.Write(" | ");

            Console.WriteLine(Description);
        }

        public static int GetPrintableLength(string str)
        {
            int length = 0;
            foreach(char c in str)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    length += 2; // 한글과 같은 넒은 문자에 대해 길이를 2로 취급
                }
                else
                {
                    length += 1; // 나머지 문자에 대해 길이를 1로 취급
                }
            }

            return length;
        }

        public static string PadRightForMixedText(string str, int totalLenth)
        {
            int currentLength = GetPrintableLength(str);
            int padding = totalLenth - currentLength;

            return str.PadRight(str.Length + padding);
        }

    }
    internal class Game2
    {
        static Character player;
        static Item[] items;
        static void Main(string[] args)
        {
            //구성
            // 0. 데이터 초기화
            // 1. 스타팅 로고를 보여줌 (게임을 처음 켤때만 보여줌)
            // 2. 선택 화면을 보여줌 (기본 구현사항 - 상태 / 인벤토리)
            // 3. 상태화면을 구현함 (필요 구현 요소 : 캐릭터, 아이템)
            // 4. 인벤토리 화면 구현함
            //    4-1. 장비장착 화면 구현
            // 5. 선택 화면 확장
            GameDataSetting();
            PrintStartLogo();
            StartMenu();
        }

        private static void StartMenu()
        {
            //구성
            // 0. 화면정리
            // 1. 선택 멘트를 줌
            // 2. 선택 결과값을 검증함
            // 3. 선택 결과에 따라 메뉴로 보내줌

            Console.Clear();
            Console.WriteLine("◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽");
            Console.WriteLine("");
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽◾◽");
            Console.WriteLine("");

            Console.WriteLine("< 스파르타 마을 >\n");
            Console.WriteLine("[활동 선택]\n");

            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("\n5. 게임 끝내기");

            // 나쁜유저들 대비
            switch(CheckValidInput(1, 5))
            {
                case 1:
                    StatusMenu();
                    break;
                case 2:
                    InventoryMenu();
                    break;
                case 5:
                    return;
            }

        }

        private static void InventoryMenu()
        {
            Console.Clear();

            Console.WriteLine("");
            ShowHighlightedText("■ 인벤토리 ■");
            Console.WriteLine("");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]"); 

            for (int i = 0; i < Item.ItemCnt; ++i)
            {
                items[i].PrintItemStatDescription();
            }
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("");
            switch (CheckValidInput(0, 1))
            {
                case 0:
                    StartMenu();
                    break;
                case 1:
                    EquipMenu();
                    break;
            }

        }

        private static void EquipMenu()
        {
            Console.Clear();

            ShowHighlightedText("■ 인벤토리 - 장착관리 ■");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < Item.ItemCnt; ++i)
            {
                items[i].PrintItemStatDescription(true, i + 1);
            }
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");

            int KeyInput = CheckValidInput(0, Item.ItemCnt);

            switch (KeyInput)
            {
                case 0:
                    InventoryMenu();
                    break;
                default:
                    ToggleEquipStatus(KeyInput - 1);
                    EquipMenu();
                    break;
            }

        }

        private static void ToggleEquipStatus(int idx)
        {
            items[idx].IsEquipped = !items[idx].IsEquipped;
        }

        private static void StatusMenu()
        {
            Console.Clear();

            Console.WriteLine("");
            ShowHighlightedText("■ 상태 보기 ■");
            Console.WriteLine("");
            Console.WriteLine("캐릭터의 정보가 표기됩니다.");

            Console.WriteLine("");
            PrintTextWithHighlights("Lv. ", player.Level.ToString("00"));// D2와 00 동일 효과           
            Console.WriteLine("{0} ( {1} )", player.Name, player.Job);

            int bonusAtk = GetSumBonusAtk();
            PrintTextWithHighlights("공격력 : ", (player.Atk + bonusAtk).ToString(), bonusAtk > 0 ? string.Format(" (+{0})", bonusAtk) : "");
            int bonusDef = GetSumBonusDef();
            PrintTextWithHighlights("방어력 : ", (player.Def + bonusDef).ToString(), bonusDef > 0 ? string.Format(" (+{0})", bonusDef) : "");
            int bonusHp = GetSumBonusHp();
            PrintTextWithHighlights("체력 : ", (player.Hp + bonusHp).ToString(), bonusHp > 0 ? string.Format(" (+{0})", bonusHp) : "");
            PrintTextWithHighlights("골드 : ", player.Gold.ToString());
            Console.WriteLine("");
            Console.WriteLine("0. 뒤로가기");
            Console.WriteLine("");

            switch (CheckValidInput(0, 0))
            {
                case 0:
                    StartMenu();
                    break;
            }

        }

        private static int GetSumBonusAtk()
        {
            int sum = 0;
            for(int i = 0; i <Item.ItemCnt; ++i)
            {
                if (items[i].IsEquipped)
                    sum += items[i].Atk;
            }
            return sum;
        }
        private static int GetSumBonusDef()
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCnt; ++i)
            {
                if (items[i].IsEquipped)
                    sum += items[i].Def;
            }
            return sum;
        }
        private static int GetSumBonusHp()
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCnt; ++i)
            {
                if (items[i].IsEquipped)
                    sum += items[i].Hp;
            }
            return sum;
        }



        private static void ShowHighlightedText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private static void PrintTextWithHighlights(string s1, string s2, string s3 = "")
        {
            Console.Write(s1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(s2);
            Console.ResetColor();
            Console.WriteLine(s3);
        }

        private static int CheckValidInput(int min, int max)
        {
            // 설명
            // 아래 두 가지 상황은 비정상 -> 재입력 수행
            // 1. 숫자가 아닌 입력을 받은 경우
            // 2. 숫자가 최소-최대 범위를 넘은 경우

            int keyInput;
            bool result;

            do
            {
                Console.Write("\n원하시는 행동을 입력해 주세요.\n>> ");
                result = int.TryParse(Console.ReadLine(), out keyInput);
            } while(result == false || CheckIfValid(keyInput, min, max) == false);

            // 여기에 왔다는 것은 제대로 입력을 받았다는 것
            return keyInput;
        }

        private static bool CheckIfValid(int keyInput, int min, int max)
        {
            if (min <= keyInput && keyInput <= max) return true;
            return false;
        }

        private static void PrintStartLogo()
        {
            Console.WriteLine("\n ===========================================================================\n");
            Console.WriteLine("   ██     ██ ███████ ██      ██       ██████  ██████  ███    ███ ███████ ");
            Console.WriteLine("   ██     ██ ██      ██      ██      ██      ██    ██ ████  ████ ██      ");
            Console.WriteLine("   ██  █  ██ █████   ██      ██      ██      ██    ██ ██ ████ ██ █████   ");
            Console.WriteLine("   ██ ███ ██ ██      ██      ██      ██      ██    ██ ██  ██  ██ ██      ");
            Console.WriteLine("    ███ ███  ███████ ███████ ███████  ██████  ██████  ██      ██ ███████ ");
            Console.WriteLine("                                                                         ");
            Console.WriteLine("                                                                         ");
            Console.WriteLine("           ███████ ██████   █████  ██████  ████████  █████               ");
            Console.WriteLine("           ██      ██   ██ ██   ██ ██   ██    ██    ██   ██              ");
            Console.WriteLine("           ███████ ██████  ███████ ██████     ██    ███████              ");
            Console.WriteLine("                ██ ██      ██   ██ ██   ██    ██    ██   ██              ");
            Console.WriteLine("           ███████ ██      ██   ██ ██   ██    ██    ██   ██              ");
            Console.WriteLine("                                                                         ");
            Console.WriteLine("                                                                         ");
            Console.WriteLine("   ██████  ██    ██ ███    ██  ██████  ███████  ██████  ███    ██        ");
            Console.WriteLine("   ██   ██ ██    ██ ████   ██ ██       ██      ██    ██ ████   ██        ");
            Console.WriteLine("   ██   ██ ██    ██ ██ ██  ██ ██   ███ █████   ██    ██ ██ ██  ██        ");
            Console.WriteLine("   ██   ██ ██    ██ ██  ██ ██ ██    ██ ██      ██    ██ ██  ██ ██        ");
            Console.WriteLine("   ██████   ██████  ██   ████  ██████  ███████  ██████  ██   ████        ");
            Console.WriteLine("\n ===========================================================================");
            Console.WriteLine("                           PRESS ANYKEY TO START                            ");
            Console.WriteLine(" ===========================================================================");
            Console.ReadKey();
        }

        private static void GameDataSetting()
        {
            player = new Character("chad", "전사", 1, 10, 5, 100, 1500);
            items = new Item[10];
            AddItem(new Item("무쇠갑옷","무쇠로 만들어져 튼튼한 갑옷입니다.",0,0,5,0));
            AddItem(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", 1, 2, 0, 0));
        }

        static void AddItem(Item item)
        {
            if (Item.ItemCnt == 10) return;
            items[Item.ItemCnt] = item;
            Item.ItemCnt++;
        }
    }
}
