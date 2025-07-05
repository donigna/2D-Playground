using UnityEngine;

namespace com.Kuwiku
{
    public enum RuleType
    {
        Age,
        City,
        Gender
    }

    public class RuleField : MonoBehaviour
    {
        // Rule Field berisi id rule,
        // nantinya akan digunakan untuk melakukan pengecekan pada data document dengan rule yang ada

        protected RuleType ruleType;

        // Aturan dari setiap rule tulis dibawah sini
        // ...

        protected virtual void RuleValidity()
        {
            Debug.Log("Rule approved!");
        }
    }
}
