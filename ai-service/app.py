import gradio as gr
from transformers import pipeline

# Sentiment Analysis modelini yükle
sentiment = pipeline(
    "sentiment-analysis",
    model="tabularisai/multilingual-sentiment-analysis",
    device=-1
)

def analyze(text: str):
    if not text or not text.strip():
        return {"label": "NEUTRAL", "score": 0.0}
    result = sentiment(text)[0]
    return {"label": result["label"], "score": float(result["score"])}

# Minimal API-only Blocks
with gr.Blocks() as demo:
    # API endpoint için input/output
    text_input = gr.Textbox()
    json_output = gr.JSON()
    
    # POST isteği geldiğinde çalışacak
    text_input.submit(fn=analyze, inputs=text_input, outputs=json_output)

# Spaces üzerinde çalıştırmak için
demo.launch(server_name="0.0.0.0", server_port=7860, show_api=True, debug=True)
