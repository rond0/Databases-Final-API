using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class ServerManager : MonoBehaviour
{
    [SerializeField] string CardName = "loatheb";
    [SerializeField] string ServerURL = "https://omgvamp-hearthstone-v1.p.rapidapi.com/cards/";
    [SerializeField] TextMeshProUGUI tmp;
    [SerializeField] public string jsonhold;
    Card card = new Card();
    RootObject wrapper = new RootObject();
    [System.Serializable]
    public class Card
    {
        public string cardId;
        public int dbfId;
        public string name;
        public string cardSet;
        public string type;
        public string rarity;
        public int cost;
        public int attack;
        public int health;
        public int durability;
        public int armor;
        public string text;
        public string flavor;
        public string artist;
        public bool collectible;
        public bool elite;
        public string race;
        public string playerClass;
        public string img;
        public string imgGold;
        public string locale;
        public Mechanic[] mechanics;


    }
    [System.Serializable]
    public class Mechanic
    {
        public string name;
    }
    [System.Serializable]
    public class RootObject
    {
        public Card[] cards;
    
    }
    // Start is called before the first frame update

    void Start()
    {
        
        card.cardId = "BOT_123";
        card.dbfId = 1255;
        card.name = "Fat Cat";
        card.cardSet = "Heavensward";
        card.type = "minion";
        card.rarity = "epic";
        card.cost = 4;
        card.attack = 4;
        card.health = 8;
        card.text = "it's a fat cat!";
        card.flavor = "chunky boi";
        card.artist = "galan";
        card.collectible = true;
        card.elite = true;
        card.race = "beast";
        card.playerClass = "RDM";
        card.img = "image url";
        card.imgGold = "gold image url";
        card.locale = "enUS";
        card.mechanics = new Mechanic[3] 
        {
        new Mechanic() { name = "cute" },
        new Mechanic() { name = "brown" },
        new Mechanic() { name = "black" }
        };

        wrapper.cards = new Card[1] {card};
      //  wrapper.cards[0] = card;

        Debug.Log(JsonUtility.ToJson(wrapper));
        StartCoroutine(GetRequest(System.Web.HttpUtility.UrlPathEncode(ServerURL + CardName + "?collectible=1")));
        

        
    }
    //public string SaveToString()
    //{
    //    return JsonUtility.ToJson(this);
    //}


    //public static Card CreateFromJSON(string jsonString)
    //{
    //    return JsonUtility.FromJson<Card>(jsonString);
    //}
    IEnumerator GetRequest(string uri)
    {
        
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            webRequest.SetRequestHeader("x-rapidapi-key", "a3eac0f1bamsh2e60b558d25caa8p153b61jsnbde12a2543bd");
            webRequest.SetRequestHeader("x-rapidapi-host", "omgvamp-hearthstone-v1.p.rapidapi.com");
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                Debug.Log((webRequest.downloadHandler.text));
                jsonhold =  (webRequest.downloadHandler.text);
                jsonhold = "{\"cards\":" + jsonhold + "}";
                Debug.Log(jsonhold);
                wrapper = JsonUtility.FromJson<RootObject>(jsonhold);
                card = wrapper.cards[0];
                Debug.Log(wrapper.cards[0].dbfId.ToString());
                if (card.type == "Minion") 
                {
                    tmp.text = $"Name: {card.name} \nType: {card.type} \nCost: {card.cost} \nAttack: {card.attack} \nHealth: {card.health} \nEffect: {card.text}";
                }
               else if (card.type == "Spell")
                {
                    tmp.text = $"Name: {card.name} \nType: {card.type} \nCost: {card.cost} \nEffect: {card.text}";
                }
                else if (card.type == "Weapon")
                {
                    tmp.text = $"Name: {card.name} \nType: {card.type} \nCost: {card.cost} \nAttack: {card.attack} \nDurability: {card.durability} \nEffect: {card.text}";
                }
                else if (card.type == "Hero")
                {
                    tmp.text = $"Name: {card.name} \nType: {card.type} \nCost: {card.cost}  \nArmor: {card.armor} \nEffect: {card.text}";
                }
               // Card wrapper = JsonUtility.FromJson<Card>(webRequest.downloadHandler.text);
            }
        }
    }
}
