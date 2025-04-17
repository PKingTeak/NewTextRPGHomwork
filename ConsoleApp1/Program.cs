using System;

class Unit
{
    protected struct UnitState
    {
        public int Hp;
        public int Damage;
        public int Level;
        public int MaxEXP;
    }

    public int GetHp()
    {
        return state.Hp;
    }

    protected UnitState state;

    public bool IsDead => state.Hp <= 0;

    public virtual void TakeDamage(int dmg)
    {
        state.Hp -= dmg;
        if (state.Hp < 0)
            state.Hp = 0;
    }

    public virtual void ShowState()
    {
        Console.WriteLine($"HP: {state.Hp}, DMG: {state.Damage}");
    }

    public int GetDamage() => state.Damage;
}

class Player : Unit
{
    public string name;

    public Player(string _name, int _hp, int _damage)
    {
        name = _name;
        state.Hp = _hp;
        state.Damage = _damage;
    }

    public override void ShowState()
    {
        Console.WriteLine($"[Player: {name}] HP: {state.Hp}, DMG: {state.Damage}");
    }
}

class Monster : Unit
{
    public string type;

    public Monster(string _type, int _hp, int _damage)
    {
        type = _type;
        state.Hp = _hp;
        state.Damage = _damage;
    }

    public override void ShowState()
    {
        Console.WriteLine($"[Monster: {type}] HP: {state.Hp}, DMG: {state.Damage}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Player player = new Player("Hero", 100, 25);
        Monster monster = new Monster("Slime", 60, 15);

        Console.WriteLine("=== 전투 시작! ===");
        while (!player.IsDead && !monster.IsDead)
        {
            Console.Clear(); // 매 턴 화면 정리 (선택 사항)

            Console.WriteLine("===== 전투 상태 =====");
            Console.WriteLine($"[플레이어] HP: {player.GetHp()}  |  [몬스터] HP: {monster.GetHp()}");
            Console.WriteLine("===========================");

            Console.WriteLine("\n▶ 플레이어의 턴!");
            Console.WriteLine("1. 일반 공격");
            Console.WriteLine("2. 스킬 공격 (2배 데미지)");

            Console.Write("선택 >> ");
            string input = Console.ReadLine();

            int damage = player.GetDamage();

            if (input == "2")
            {
                Console.WriteLine("\n 스킬 공격 발동!");
                damage *= 2;

                Console.WriteLine($"{damage}  의 데미지로 공격");

            }
            else
            {
                Console.WriteLine("\n 일반 공격!");
                Console.WriteLine($"{damage}의 데미지로 공격");

            }

            monster.TakeDamage(damage);

            if (monster.IsDead)
            {
                Console.WriteLine("\n✅몬스터를 처치했습니다!");
                break;
            }

            Console.WriteLine("\n▶ 몬스터의 반격!");
            player.TakeDamage(monster.GetDamage());

            if (player.IsDead)
            {
                Console.WriteLine("\n💀 플레이어가 쓰러졌습니다...");
                break;
            }

            Console.WriteLine("\n[Enter]를 눌러 다음 턴으로 진행하세요...");
            Console.ReadLine();
        }


        Console.WriteLine("=== 전투 종료 ===");
    }
}
