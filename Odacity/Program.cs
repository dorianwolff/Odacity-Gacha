using Odacity;

LoadScreen loadScreen = new LoadScreen();
Account account = new Account("",0,new List<Character>(),new List<Character>(),
    1);

string selectedAccount = loadScreen.ShowLoadScreen(); //returns the path of the account

if (LoadScreen.isNewAccount)
{
    account.SummoningCurrency = 200;
}
//Loading.ShowLoadingScreen();
account = Account.LoadAccountData(selectedAccount);
// Menu Start
Menu.characterCollection = account.CharacterCollection;
Menu.summoningCurrency = account.SummoningCurrency;
Menu.summonableCharacters = account.SummonableCharacters;
Menu.User = account.Username;
//Dungeon Start
Dungeon.towerStageClear = account.TowerStageClear;
Console.CancelKeyPress += (sender, e) => Account.Console_CancelKeyPress(sender, e, selectedAccount,account);
Dungeon.Team = new List<Character>();
foreach (Character character in Menu.characterCollection)
{
    if (character.onTeam)
        Dungeon.Team.Add(character);
}
Menu.ShowMenu();

//Menu Close
account.SummoningCurrency = Menu.summoningCurrency;
account.CharacterCollection = Menu.characterCollection;
account.SummonableCharacters = Menu.summonableCharacters;
//Dungeon Close
account.TowerStageClear = Dungeon.towerStageClear;

Account.SaveAccountData(selectedAccount, account);