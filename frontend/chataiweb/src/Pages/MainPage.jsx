import axios from "axios";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { toast } from "react-toastify";
import { API_BASE_URL } from "../config";

export function MainPage() {
  const [messages, setMessages] = useState([]);
  const [messageText, setMessageText] = useState("");
  const [sendMessage, setSendMessage] = useState(false);

  const navigate = useNavigate();

  useEffect(() => {
    axios
      .get(`${API_BASE_URL}/api/Message`, {
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
        withCredentials: true,
      })
      .then((response) => {
        setMessages(response.data);
        setSendMessage(false);
      });
  }, [sendMessage]);

  async function SendMessage(messageText) {
    try {
      axios
        .post(
          `${API_BASE_URL}/api/Message`,
          { messageText: messageText },
          {
            headers: {
              Accept: "application/json",
              "Content-Type": "application/json",
              Authorization: `Bearer ${localStorage.getItem("token")}`,
            },
            withCredentials: true,
          },
        )
        .then((response) => {
          setMessageText("");
          setSendMessage(true);
        });
    } catch (error) {
      toast.error("Mesaj gönderilemedi.");
    }
  }

  function LogOut() {
    localStorage.removeItem("token");
    navigate("/login");
  }

  return (
    <section className="bg-linear-to-l from-sky-500 to-indigo-500 w-full min-h-screen flex justify-center px-4">
      <div className="lg:flex lg:w-full lg:mr-[40%] lg:justify-between">
        <div className="hidden lg:flex lg:w-[20%] lg:justify-end lg:flex-col">
          <button
            className="hidden lg:block lg:text-2xl lg:text-amber-400 lg:border-amber-400 lg:bg-amber-950 
          lg:border-6 lg:rounded-full lg:text-center lg:p-4 lg:font-bold lg:align-middle lg:cursor-pointer "
            onClick={LogOut}
          >
            Logout
          </button>
        </div>
        <div className="flex flex-col">
          <div className="flex gap-2">
            <button
              className="bg-red-600 font-bold text-white rounded-full px-4 py-2 mt-2 md:hidden"
              onClick={LogOut}
            >
              X
            </button>
            <h1 className="text-white w-full font-bold sm:text-lg md:text-xl mt-2 rounded-2xl border-amber-300 p-2 px-10 sm:p-3 sm:px-14 md:p-4 md:px-16 bg-linear-to-l from-amber-400 to-indigo-200 max-w-lg text-center">
              Mesajlar
            </h1>
          </div>

          <div className="flex flex-col gap-2">
            {messages.map((message) => (
              <div
                key={message.id}
                className="bg-white rounded-lg p-4 m-2 shadow-md"
              >
                <h2 className="text-lg font-semibold m-0 p-0">
                  Kullanıcı: {message.username}
                </h2>
                <p className="text-lg  font-semibold">{message.messageText}</p>
                <p className="text-lg font-semibold">
                  Message Status: {message.sentimentLabel}
                </p>
                <p className="text-lg font-semibold">
                  Score: {message.sentimentScore}
                </p>
              </div>
            ))}
          </div>
          <div className="flex sticky">
            <input
              type="text"
              placeholder="Mesaj giriniz..."
              className="p-2 rounded border-2 border-gray-300 w-full align-middle text-lg sm:text-xl md:text-2xl bottom"
              onChange={(e) => setMessageText(e.target.value)}
              value={messageText}
            />
            <button
              onClick={() => SendMessage(messageText)}
              className="rounded-full w-14 bg-amber-600 cursor-pointer text-white font-bold"
            >
              G
            </button>
          </div>
        </div>
      </div>
    </section>
  );
}
