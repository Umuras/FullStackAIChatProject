import axios from "axios";
import { useForm } from "react-hook-form";
import { Link, useNavigate } from "react-router";
import { toast } from "react-toastify";

export function LoginPage() {
  const navigate = useNavigate();

  const {
    register,
    handleSubmit,
    formState: { errors, isValid },
  } = useForm({
    defaultValues: {
      username: "",
      description: "loginPage",
    },
    mode: "all",
  });

  async function submitForm(formData) {
    try {
      const response = await axios
        .post("http://localhost:5239/api/Auth/login", formData)
        .then((response) => {
          console.log("Kayıt Başarılı:", response.data);
          toast.success("Kayıt Başarılı! Ana sayfaya yönlendiriliyorsunuz...");
          navigate("/mainpage");
        });
    } catch (error) {
      toast.error("Giriş başarısız. Kullanıcı adı bulunamadı.");
      console.error("Kayıt Hatası:", error);
      console.log("Form Data Hatası:", formData);
    }
  }

  return (
    <section className="bg-linear-to-l from-sky-500 to-indigo-500 w-full min-h-screen flex items-center justify-center px-4">
      <div className="flex flex-col justify-center gap-6 w-full max-w-md">
        <h1 className="text-2xl sm:text-3xl md:text-4xl text-amber-300 font-bold text-center">
          Mesajlaşma Uygulamasına Hoş Geldin
        </h1>
        <h2 className="text-xl sm:text-2xl md:text-3xl text-amber-600 font-bold text-center">
          Giriş Sayfası
        </h2>
        <form
          className="flex flex-col gap-4"
          onSubmit={handleSubmit(submitForm)}
        >
          <input
            type="text"
            placeholder="Kullanıcı Adı"
            {...register("username", {
              required: "Kullanıcı adı gerekli",
              minLength: { value: 3, message: "En az 3 karakter olmalı" },
            })}
            className="p-2 rounded border-2 border-gray-300 w-full align-middle text-lg sm:text-xl md:text-2xl"
          />
          {errors.username && (
            <p className="text-red-500 text-sm sm:text-lg md:text-2xl">
              {errors.username.message}
            </p>
          )}

          <button
            className="text-lg sm:text-xl md:text-2xl bg-blue-500 text-white rounded-2xl w-full py-2 hover:bg-blue-600 transition-colors cursor-pointer"
            type="submit"
            disabled={!isValid}
          >
            Giriş Yap
          </button>
          <Link
            className="text-lg sm:text-xl md:text-2xl cursor-pointer text-amber-300 font-bold text-center hover:underline"
            to="/register"
          >
            Eğer hesabın yoksa önce hesap oluştur
          </Link>
        </form>
      </div>
    </section>
  );
}
