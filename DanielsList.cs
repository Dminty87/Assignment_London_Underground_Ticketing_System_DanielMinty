using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Daniel Minty
// Assignment: Lond Underground Communt
// 01/10/2024

namespace Assignment_London_Underground_Ticketing_System
{
    //Instruction: make it generic but Type safe to Rider. But can't be Rider only.
    //DanielsList implements IEnumerable. Acceptable types for T are Rider and children of Rider.
    public class DanielsList<T> : IEnumerable<T> where T : Rider
    {

        //Array to store all the list's items
        private T[] items;

        //The number of filled indicies in the array
        private int count;

        //Property to allow public access to count
        public int Count => count;

        public DanielsList() : this(10) { }
        //Creates a list with initialSize of ten

        public DanielsList(int initialSize)
        {//Create a list with specified initialSize
            items = new T[initialSize];
        }

        private void CheckIntegrity()
        {//Make sure that the list has enough space to hold the any new items
            //If the list is at least 80% full,
            if (count >= 0.8 * items.Length)
            {//increase it's maximum capacity by two by copying the current array into a larger array
                T[] largerArray = new T[items.Length + 2];
                Array.Copy(items, largerArray, count);
                items = largerArray;
            }
        }

        public void Add(T item)
        {//Adds an item to the end of the list
            //Make sure that the list has enough space to hold the new item
            CheckIntegrity();
            //Add the item to the end of the list and increment count.
            items[count++] = item;
        }

        public void AddAtIndex(T item, int index)
        {//Insert the specified item at the specified index
            //Make sure that the list has enough space to hold the new item
            CheckIntegrity();
            for (int i = count - 1; i >= index; i--)
            {//from the last index, move each item down until the desired index is reached
                items[i + 1] = items[i];
            }
            //insert the item at the desired index
            items[index] = item;
            //increment count
            count++;
        }

        public void RemoveAtIndex(int index)
        {//Remove the item at the given index and shift items toward the front to fill the gap
            //Check if the index is out of bounds
            if (index < 0 || index >= count)
            {//If the index is out of bounds, return to prevent a crash.
                return;
            }
            //For each element after the desired index,
            for (int i = index + 1; i < count; i++)
            {//Move that element one index toward the beginning, overriding old elements
                items[i - 1] = items[i];
            }
            //one less item means decrement count
            count--;
        }

        public T? GetItem(int index)
        {//Return the item at the specified index
            //Check if the index is out of bounds
            if (index < 0 || index >= count)
            {//If the index is out of bounds
                //prevent crashing by return default(T) instead of throwing an exeption
                return default(T);
            }
            //in bounds, return value at index
            return items[index];
        }

        //Not sure exactly enumerators work but i'll try to comment.
        //This gets called by the system when an enumerator is needed for something?
        IEnumerator IEnumerable.GetEnumerator() //I don't understand this line
        {
            //returns the enumerator from DanielsList.GetEnumerator()
            return this.GetEnumerator();
        }

        //DanielsList.GetEnumerator() returns the enumerator for this class?
        public IEnumerator<T> GetEnumerator()
        {
            //This for loop is the enumerator?
            for (int i = 0; i < count; i++)
            {
                yield return items[i];
            }
        }

        public DanielsList<T> ReturnRidersAtStation(int station)
        {//Returns a list of Riders that started at the specified station.
            //List to be returned
            DanielsList<T> result = new DanielsList<T>();
            foreach (T item in this)
            {//for each item in the list,
                //check if the item's station is the desired station
                if ((int)item.StationOn == station)
                {//if stations match, add the item to the result set
                    result.Add(item);
                }
            }
            //When the search is done, return the results
            return result;
        }

        public DanielsList<T> ReturnRidersAtStation(Station station)
        {//Returns a list of Riders that started at the specified station.
            //Convert the Station to an int and use the existing method
            return this.ReturnRidersAtStation((int)station);
        }

        public DanielsList<T> ReturnAllActiveRiders()
        {//Returns a list of riders that haven't gotten off yet
            //List to be returned
            DanielsList<T> result = new DanielsList<T>();
            foreach (T item in this)
            {//for each item in the list,
                //check if the rider is active
                if (item.IsActive)
                {
                    //if they are, add them to the result set
                    result.Add(item);
                }
            }
            //When the search is done, return the results
            return result;
        }

    }//End of DanielsList
}//End of Namespace
