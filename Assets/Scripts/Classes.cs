using UnityEngine;
using UnityEngine.SceneManagement;

namespace Classes {
    public class Choice<T> {
        public int choice {
            get => choiceee;
            set {
                choiceee = value;
                if (choiceee > choices.Length-1) {
                    choiceee = choices.Length - choiceee;
                }
            }
        }
        public T[] choices;
        private int choiceee;
        public Choice(T[] choicess) {
            choices = choicess;
            choice = 0;
        }
        public Choice(T[] choicess, int choicee) {
            choices = choicess;
            choice = choicee;
        }
        public T Get {
            get {
                return choices[choice];
            }
        }
    }
    [SerializeField]
    public class Requirement {
        public int Max;
        public bool Met;
#pragma warning disable IDE0044 // Add readonly modifier
        private int num = 0;
        private bool Perm;
#pragma warning restore IDE0044 // Add readonly modifier
        public int Num {
            get => num;
            set {
                num = value;
                if(num >= this.Max) {
                    Met = true;
                } else if(!Perm) {
                    Met = false;
                }
            }
        }
        public Requirement(int max) { Max = max; }
        public Requirement(int max, bool perm) { Max = max; Perm = perm; }
    }
    [SerializeField]
    public class Mitochondria {
        public bool Done;
        public Mitochondria() { }
        public Requirement co2 = new Requirement(6, true);
        public Requirement h2o = new Requirement(12, true);
        public void Respirate(Requirement c,Requirement w) {
            if(c.Met && w.Met) {
                SceneManager.LoadSceneAsync("Win");
            }
        }
    }
}
