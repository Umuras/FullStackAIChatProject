import gradio as gr
from transformers import pipeline

# Sentiment Analysis modelini yükle (CPU)
sentiment = pipeline(
    "sentiment-analysis",
    model="cardiffnlp/twitter-xlm-roberta-base-sentiment",
    device=-1
)

def analyze(text: str):
    if not text or not text.strip():
        return {"label": "NEUTRAL ", "score": 0.0}
    result = sentiment(text)[0]
    return {
        "label": result["label"],   # POSITIVE / NEGATIVE
        "score": float(result["score"])
    }

# Gradio UI ve API
with gr.Blocks() as demo:
    gr.Markdown("#Sentiment Analysis API (Hugging Face Spaces)")
    
    inp = gr.Textbox(label="Message", placeholder="Enter a message...")
    out = gr.JSON(label="Result")
    btn = gr.Button("Analyze")
    
    btn.click(fn=analyze, inputs=inp, outputs=out)

if __name__ == "__main__":
    demo.launch()
