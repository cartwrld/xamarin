using System;
using System.Collections.Generic;
using System.Text;

namespace EX06NavigationPassing
{
    public class Person
    {
        public string firstName;
        public string lastName;

        public Person(string fn, string ln) 
        {
            this.firstName = fn;
            this.lastName = ln;
        }

        public string getFn() { return firstName; }
        public string getLn() { return lastName; }
        public void setLn(string nln) { lastName = nln; }


    }
}
