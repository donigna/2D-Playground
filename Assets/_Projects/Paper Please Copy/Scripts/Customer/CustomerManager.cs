using System.Collections.Generic;
using UnityEngine;

namespace com.Kuwiku
{
    public class CustomerManager : MonoBehaviour
    {
        public static CustomerManager Instance;
        // Customer Manager
        // Code ini digunakan sebagai wadah Customer yang muncul dalam satu hari
        // List Customer akan regenerasi setiap 1 hari
        public Customer customerPrefabs;
        public CustomerRegularData regularCustomers;
        public List<Customer> uniqueCustomers;


        public void Start()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
        }

        public CustomerData GenerateRegularCustomerData()
        {
            CustomerData newCustomer = new CustomerData
            {
                name = regularCustomers.possibleNames[Random.Range(0, regularCustomers.possibleNames.Count)],
                face = regularCustomers.possibleFaces[Random.Range(0, regularCustomers.possibleFaces.Count)],
                country = regularCustomers.possibleCountries[Random.Range(0, regularCustomers.possibleCountries.Count)],
                dialogue = regularCustomers.possibleDialogues[Random.Range(0, regularCustomers.possibleDialogues.Count)],
            };
            return newCustomer;
        }

        public void CreateNewCustomer()
        {
            if (LevelManager.Instance.GetStoreOpenStatus())
            {
                Customer newCustomer = Instantiate(customerPrefabs);
                newCustomer.InitializeData(GenerateRegularCustomerData());
                Debug.Log("New Customer Created!");
            }
        }

        public void SpawnUniqueCustomer(string customerID)
        {
            Customer unique = uniqueCustomers.Find(c => c.CustomerID == customerID);
            if (unique.IsUnique)
            {
                Instantiate(unique);
                Debug.Log("Unque customer found!");
            }
            else
            {
                Debug.Log("Unique customer not found!");
            }
        }
    }
}
