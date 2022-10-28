using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB
{
    
    public class FriendsMananger : MonoBehaviour
    {
        public static FriendsMananger Instance;
        private void Awake()
        {
            Instance = this;
        }
        [SerializeField] private List<Friend> _allFriends = new List<Friend>();
        [SerializeField] private Transform _friendSlotOne;
        [SerializeField] private Transform _friendSlotTwo;
        private List<Friend> _assignedFriends = new List<Friend>();
        private void Start()
        {
            var firstFriend = _allFriends[Random.Range(0,_allFriends.Count)];
            _allFriends.Remove(firstFriend);
            _assignedFriends.Add(firstFriend);
            Instantiate(firstFriend,_friendSlotOne);
            firstFriend.SetCanvasCamera();

            var secondFriend = _allFriends[Random.Range(0,_allFriends.Count)];
            _allFriends.Remove(secondFriend);
            _assignedFriends.Add(secondFriend);
            Instantiate(secondFriend,_friendSlotTwo);
            secondFriend.SetCanvasCamera();
        }
    }
}
