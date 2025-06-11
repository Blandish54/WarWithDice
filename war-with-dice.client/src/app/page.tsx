'use client'

import { PlayDiceRoundResponse } from "./models/PlayDiceRoundResponse";
import React, { MouseEventHandler } from 'react';
import { useState } from 'react';
import { CreateGameUserRequest } from "./models/CreateGameUserRequest";
import axios, { AxiosError } from 'axios';


export default function GamePage() {


  function handleRollDiceClick(event: React.MouseEvent<HTMLButtonElement>) {

    try {
      axios.post('https://localhost:7299/GameUser/Add');

    }
    catch (error) {
      if (axios.isAxiosError(error)) {
        const axiosError = error as AxiosError

        console.log('Axios Error:' + axiosError.message)
      }
    }
  };


  return (
    <div className="h-screen grid grid-rows-[auto_1fr_auto] bg-gray-200">
      <header className="bg-gray-800 text-white p-4 shadow-md">
         <h1>
          War With Dice
         </h1>
      </header>

      <main className="grid grid-cols-1 md:grid-cols-3 gap-4 p-4 overflow-y-auto">
        
        <div className="bg-white p-6 rounded-lg shadow">
          <h1 >Player 1 Total Dice</h1>
        </div>

        <div className="bg-white p-6 rounded-lg shadow">
          <h1 className="text-center">Game Field</h1>
        </div>

        <div className="bg-white p-6 rounded-lg shadow">
          <h1>Player 2 Total Dice</h1>
        </div>

        
      </main>

      <footer className="bg-gray-800 text-white p-4 flex items-center justify-between h-20">
  <div className="w-1/4" /> 
  <button className="bg-gray-300 text-gray-800 py-2 px-4 rounded">
    Roll Dice
  </button>
  <h1 className="text-xl">Total Rolls:</h1>
</footer>
    </div>

  )
}
