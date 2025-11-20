import { useEffect } from "react";
import { useNavigate } from "react-router";
import { toast } from "react-toastify";

export function ProtectedRoute({ children }) {
  const token = localStorage.getItem("token");
  const navigate = useNavigate();

  useEffect(() => {
    if (!token) {
      toast.error("Mesajlaşma uygulamasına erişim için giriş yapmalısınız.");
      navigate("/login");
    }
  }, [token, navigate]);

  // Eğer token yoksa, yönlendirme yapılmadan önce boş bir şey dön
  if (!token) {
    return null;
  }

  return children;
}
