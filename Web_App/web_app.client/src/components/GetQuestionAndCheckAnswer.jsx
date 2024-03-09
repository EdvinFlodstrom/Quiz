import React, { useState } from "react";

const GetQuestionAndCheckAnswer = ({ name, numQuestions }) => {
    const buttonEnabledClassName = "instruction-button";
    const buttonDisabledClassName = "instruction-button-disabled";

    const [initButtonDisabled, setInitButtonDisabled] = useState(false);
    const [getQuestionButtonDisabled, setgetQuestionButtonDisabled] = useState(false);
    const [getQuestionButtonClassName, setgetQuestionButtonClassName] = useState(buttonEnabledClassName);
    const [displayAnswer, setDisplayAnswer] = useState(false);
    const [question, setQuestion] = useState(null);
    const [writtenAnswer, setWrittenAnswer] = useState('');

    const handleInitializeQuiz = async () => {
        try {
            await fetch(`https://localhost:7140/api/quiz/initquiz/${name}/${numQuestions}`);
            setInitButtonDisabled(true);
            console.log('quiz initialized');
          } catch (error) {
            console.error('Initialization error:', error);
          }
    }

    const handleGetQuestion = async () => {
        setgetQuestionButtonDisabled(true);
        setgetQuestionButtonClassName(buttonDisabledClassName);
        const response = await fetch(`https://localhost:7140/api/quiz/takequiz/${name}`);
        setQuestion(await response.json());
        setDisplayAnswer(true);
    }

    const handleSubmitAnswer = async (answer) => {
        
    }

    return (
        <>

        {!initButtonDisabled ? (
            <button 
            className={buttonEnabledClassName}
            onClick={handleInitializeQuiz}
            disabled={initButtonDisabled}>
                Initialize Quiz
            </button>
        )
        : (
             <button
             className={getQuestionButtonClassName}
             onClick={handleGetQuestion}
             disabled={getQuestionButtonDisabled}>
                Get Question
             </button>
            )}

        {displayAnswer && question && (
            <>
            <h3>{question.questionText}</h3>

            {question.questionType === 'QuestionCard' && (
                <>
                    <label htmlFor="writtenAnswer">Input answer:</label>
                        <input
                            type="text"
                            id="writtenAnswer"
                            value={writtenAnswer}
                            onChange={(e) => setWrittenAnswer(e.target.value)}
                        />
                    <button
                    className={!writtenAnswer.trim().length ? buttonDisabledClassName : buttonEnabledClassName}
                    disabled={!writtenAnswer.trim().length}
                    onClick={() => handleSubmitAnswer(writtenAnswer)}>
                        Submit Answer
                    </button>
                </>
            )}

            {question.questionType === "MCSACard" && (
                <div>
                    <button
                    className={buttonEnabledClassName}
                    onClick={() => handleSubmitAnswer(question.option1)}>
                        {question.option1}
                    </button>
                    <button
                    className={buttonEnabledClassName}
                    onClick={() => handleSubmitAnswer(question.option2)}>
                        {question.option2}
                    </button>
                    <button 
                    className={buttonEnabledClassName}
                    onClick={() => handleSubmitAnswer(question.option3)}>
                        {question.option3}
                    </button>
                    <button 
                    className={buttonEnabledClassName}
                    onClick={() => handleSubmitAnswer(question.option4)}>
                        {question.option4}
                    </button>
                    <button
                    className={buttonEnabledClassName}
                    onClick={() => handleSubmitAnswer(question.option5)}>
                        {question.option5}
                    </button>
                </div>
            )}

            </>
        )}
        
        </>
    );
};

export default GetQuestionAndCheckAnswer;