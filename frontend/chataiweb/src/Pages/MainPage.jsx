import axios from "axios";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { toast } from "react-toastify";

export function MainPage() {
  const [messages, setMessages] = useState([]);
  const [messageText, setMessageText] = useState("");
  const [sendMessage, setSendMessage] = useState(false);

  const navigate = useNavigate();

  useEffect(() => {
    axios
      .get("http://localhost:5239/api/Message", {
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
        console.log("Mesajlar:", response.data);
      });
  }, [sendMessage]);

  async function SendMessage(messageText) {
    try {
      console.log("Gönderilen Mesaj:", messageText);
      axios
        .post(
          "http://localhost:5239/api/Message",
          { messageText: messageText },
          {
            headers: {
              Accept: "application/json",
              "Content-Type": "application/json",
              Authorization: `Bearer ${localStorage.getItem("token")}`,
            },
            withCredentials: true,
          }
        )
        .then((response) => {
          console.log("Mesaj gönderildi:", response.data);
          setMessageText("");
          setSendMessage(true);
        });
    } catch (error) {
      toast.error("Mesaj gönderilemedi.");
      console.error("Mesaj gönderme hatası:", error);
    }
  }

  function LogOut() {
    localStorage.removeItem("token");
    navigate("/login");
  }

  return (
    <section className="bg-linear-to-l from-sky-500 to-indigo-500 w-full min-h-screen flex justify-center px-4">
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
              key={message.Id}
              className="bg-white rounded-lg p-4 m-2 shadow-md"
            >
              <h2 className="text-lg font-semibold mb-2">
                Kullanıcı: {message.username}
              </h2>
              <p className="text-lg  font-semibold">{message.messageText}</p>
              <p className="">Message Status: {message.sentimentLabel}</p>
              <p className="">Score: {message.sentimentScore}</p>
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
    </section>
  );
}
