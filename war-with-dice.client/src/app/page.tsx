'use client'

import React, { MouseEventHandler } from "react";
import { useState } from "react";

///First and foremost a form
///
interface FormDataProps {
  title: string;
  disabled: boolean;

}




export default function HomePage() {
  
  const colorOptions = ["red", "blue", "green", "purple"];

  const [colorSelection, setColorSelection] = useState('');

  function handleColorSelection (event: React.MouseEvent<HTMLButtonElement>){
    setColorSelection(colorSelection);
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
              <input type="text" name="first-name" id="first-name" className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6">
              </input>
            </div>
          </div>
  
          <div className="sm:col-span-3">
            <label htmlFor="last-name" className="block text-sm/6 font-medium text-gray-900">Last name</label>
            <div className="mt-2">
              <input type="text" name="last-name" id="last-name" className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6">
              </input>
            </div>
          </div>
          
          <div className="sm:col-span-2 sm:col-start-1">
            <label htmlFor="city" className="block text-sm/6 font-medium text-gray-900">Lucky Number</label>
            <div className="mt-2">
              <input type="text" name="city" id="city" className="block w-40 rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6">
              </input>
            </div>
          </div>
        </div>
      </div>
  
      <div className="border-b border-gray-900/10 pb-12">
        <h2 className="text-base/7 font-semibold text-gray-900">Time to select a color</h2>
        <p className="mt-1 text-sm/6 text-gray-600">This will be used to add a little flair to your UserName</p>
        <div className="swatch-container">
         
        </div>

      </div>
    
  
    <div className="mt-6 flex items-center justify-end gap-x-6">
      <button type="button" className="text-sm/6 font-semibold text-gray-900">Cancel</button>
      <button type="submit" className="rounded-md bg-indigo-600 px-3 py-2 text-sm font-semibold text-white shadow-xs hover:bg-indigo-500 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600">Save</button>
    </div>
  </form>
  );
}
