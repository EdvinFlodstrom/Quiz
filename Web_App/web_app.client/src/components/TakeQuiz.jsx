import React, { useState } from "react";
import GetQuestionAndCheckAnswer from './GetQuestionAndCheckAnswer';

const TakeQuiz = () => {
    const [name, setName] = useState("");
    const [numQuestions, setNumQuestions] = useState("");
    const [showQuestions, setShowQuestions] = useState(false);
    const [error, setError] = useState("");

    const handleSubmit = () => {
        if (!name || name.length > 30) {
            setError("Please enter a valid name (max 30 characters).");
            return;
        }

        const parsedNumQuestions = parseInt(numQuestions, 10);
        if (isNaN(parsedNumQuestions) || parsedNumQuestions <= 0 || parsedNumQuestions > 10) {
            setError("Please enter a valid number of questions (1-10).");
            return;
        }

        // If validation passes, show the questions
        setShowQuestions(true);
        setError("");
    };

    return (
        <div>
            <h3>Welcome to the quiz! To get your first question, enter your name and number of questions, and then press the button below.</h3>
            
            <div>
                <label htmlFor="name">Enter name:</label>
                <input
                    type="text"
                    id="name"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                />
            </div>

            <div>
                <label htmlFor="numQuestions">Enter your amount of questions:</label>
                <input
                    type="text"
                    id="numQuestions"
                    value={numQuestions}
                    onChange={(e) => setNumQuestions(e.target.value)}
                />
            </div>

            <button className="instruction-button" onClick={handleSubmit}>
                Submit name and number of questions
            </button>

            {error && <p style={{ color: "red" }}>{error}</p>}

            {showQuestions && (
                <GetQuestionAndCheckAnswer
                    name={name}
                    numQuestions={numQuestions}
                />
            )}
        </div>
    );
};

export default TakeQuiz;
