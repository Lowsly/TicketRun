using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FrasesGO : MonoBehaviour
{
    public TextMeshProUGUI phraseText;
    public string[] phrases= {
    "Preservar esta especie marina es importante, es una vida. ",
    "¡Sé más cuidadoso con ellas! ",
    "Las tortugas marinas son guardianes del océano, protegerlas es proteger nuestro hogar. ",
    "El plástico es un material que tarda cientos de años en degradarse, evita su uso. ",
    "El cambio climático es una realidad, cada acción cuenta para frenarlo. ",
    "Cuidemos de ellas, es nuestra responsabilidad su existencia en la tierra. ",
    "La belleza del océano reside en sus habitantes. ¡Cuidemos de ellos con amor y respeto! ",
    "Las tortugas marinas son seres vivos que merecen respeto y cuidado. ",
    "El océano es un ecosistema frágil, cuidemos de él. ",
    "Si amas tu vida, empieza por amar la del planeta. ¡Cuídalo! "
    };

    void Start()
    {
        ShowRandomPhrase();
    }

    void ShowRandomPhrase()
    {
        int randomIndex = Random.Range(0, phrases.Length);
        phraseText.text = phrases[randomIndex];
    }
}

