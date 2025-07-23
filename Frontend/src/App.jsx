import { useState } from 'react'
import './App.css'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
    <div>
      <p>Enter</p>
    </div>

    <div>
      <ul>
        <li>
          <button onClick={() => setCount((count) => count + 1)}>
            BUTTON
            </button> 
        </li>
      </ul>
    </div>
    </>
  )
}

export default App
