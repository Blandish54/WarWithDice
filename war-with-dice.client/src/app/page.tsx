'use client'

import React, { MouseEventHandler } from 'react';
import { useState } from 'react';
import { CreateGameUserRequest } from "./models/CreateGameUserRequest";
import axios from 'axios';


export default function HomePage() {
  
  const colors = ["red", "blue", "green", "purple"];

  const [selectedColor, setColorSelection] = useState<string>('');

  const [firstName, setFirstName] = useState<string>('');

  const [lastName, setLastName] = useState<string>('');
  
  const [luckyNumber, setLuckyNumber] = useState<number>(0);
  /// Client side validation 

  const allowedCharacters = /^[A-Za-z\s'-]+$/;

  function test(){
    
  };

  function handleColorSelection (color: string){
    setColorSelection(color);
  };

  function handleFirstNameChange(event: React.ChangeEvent<HTMLInputElement>){
    setFirstName(event.target.value);
  };

  const firstInitial = firstName.charAt(0);

  function handleLastNameChange(event: React.ChangeEvent<HTMLInputElement>){
    setLastName(event.target.value);
  };

  function handleLuckyNumberChange(event: React.ChangeEvent<HTMLInputElement>){
    setLuckyNumber(event.target.value as any);
  };

  function handleSaveClick (event: React.MouseEvent<HTMLButtonElement>){
    
    const errors: {firstName?: string, lastName?: string} = {};

    if(!firstName.trim()) {
      errors.firstName = "First name is required";
    }
    else if(allowedCharacters.test(firstName.trim())){
      errors.firstName = "First name must only contain letters, hyphens and spaces.";
    }

    if(!firstName.trim()) {
      errors.lastName = "Last name is required";
    }
    else if(allowedCharacters.test(firstName.trim())){
      errors.lastName = "Last name must only contain letters, hyphens and spaces.";
    }
    
    const userName: string = `${firstInitial}${lastName}${luckyNumber}`;
    axios.post(`https://localhost:7299/GameUser/Add/${userName}`);
  };

  
  return (
    <form>
      <div className="border-b border-gray-900/10 pb-12">
        <h2 className="text-base/7 font-semibold text-gray-900">UserName Creation</h2>
        <p className="mt-1 text-sm/6 text-gray-600">Please fill out all the fields below and we will use your inputs to create your UserName</p>
  
        <div className="mt-10 grid grid-cols-1 gap-x-6 gap-y-8 sm:grid-cols-6">
          <div className="sm:col-span-3">
            <label htmlFor="first-name" className="block text-sm/6 font-medium text-gray-900">First name</label>
            <div className="mt-2">
              <input type="text" name="first-name" required id="first-name" value={firstName} className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6" onChange={handleFirstNameChange}>
              </input>
            </div>
          </div>
  
          <div className="sm:col-span-3">
            <label htmlFor="last-name" className="block text-sm/6 font-medium text-gray-900">Last name</label>
            <div className="mt-2">
              <input type="text" name="last-name" required id="last-name" value={lastName} className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6" onChange={handleLastNameChange}>
              </input>
            </div>
          </div>
          
          <div className="sm:col-span-2 sm:col-start-1">
            <label htmlFor="lucky-number" className="block text-sm/6 font-medium text-gray-900">Lucky Number</label>
            <div className="mt-2">
              <input type="number" name="lucky-number" required id="lucky-number" className="block w-40 rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6" value={luckyNumber} onChange={handleLuckyNumberChange}>
              </input>
            </div>
          </div>
        </div>
      </div>
  
      <div className="border-b border-gray-900/10 pb-12">
        <h2 className="text-base/7 font-semibold text-gray-900">Time to select a color</h2>
        <p className="mt-1 text-sm/6 text-gray-600">This will be used to add a little flair to your UserName</p>
        <div className="swatch-container">
          <div className="flex gap-2">
      {colors.map((color) => (
        <button
          type='button'
          key={color}
          onClick={() => handleColorSelection(color)}
          className={`w-40 h-40 rounded-full border-2 ${
            selectedColor === color ? "border-black" : "border-transparent"
          }`}
          style={{ backgroundColor: color }}
        ></button>
      ))}
      {selectedColor && (
        <div className="ml-4 text-sm">
          Selected: <span className="font-semibold">{selectedColor}</span>
        </div>
      )}
    </div>
        </div>
      </div>
    <div className="mt-6 flex items-center justify-end gap-x-6">
      <button onClick={test} type="button" className="text-sm/6 font-semibold text-gray-900">Cancel</button>
      <button type="submit" onClick={handleSaveClick} className="rounded-md bg-indigo-600 px-3 py-2 text-sm font-semibold text-white shadow-xs hover:bg-indigo-500 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600">Save</button>
    </div>
  </form>
  );
}
