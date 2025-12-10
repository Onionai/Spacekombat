using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TriesCounterPanel : MonoBehaviour
{
    private bool startCounting;
    private TriesCounterManager manager;
    private WaitForSeconds waitForSeconds;
    private WaitForSeconds sliderWaitSeconds;

    [Header("UI")]
    public Slider[] triesBarArray;
    public TextMeshProUGUI triesTimeCounter;

    private Coroutine sliderCoroutine;
    private Coroutine countdownCoroutine;

    private void OnEnable()
    {
        manager = TriesCounterManager.Instance;

        manager.OnTriesChanged -= HandleTriesChanged;
        manager.OnTriesFullyRefilled -= HandleRefill;

        manager.OnTriesChanged += HandleTriesChanged;
        manager.OnTriesFullyRefilled += HandleRefill;

        RefreshAll();
        manager.SetCounterPanel(this);
        startCounting = (manager.CurrentTriesNumber < manager.GetMaxBars());
    }

    private void Start()
    {
        foreach (var s in triesBarArray)
        {
            s.maxValue = 1f;
            s.wholeNumbers = false;
            s.value = 0f;
        }
        waitForSeconds = new WaitForSeconds(1.0f);
        sliderWaitSeconds = new WaitForSeconds(20.0f);
    }

    private void OnDisable()
    {
        if (manager != null)
        {
            manager.OnTriesChanged -= HandleTriesChanged;
            manager.OnTriesFullyRefilled -= HandleRefill;
            manager = null;
        }

        if (sliderCoroutine != null)
        {
            StopCoroutine(sliderCoroutine);
            sliderCoroutine = null;
        }
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
        }
    }

    private void HandleTriesChanged()
    {
        startCounting = true;
        UpdateCounterUI();
    }

    public void UpdateCounterUI()
    {
        if (startCounting != true || manager == null)
        {
            return;
        }

        RefreshAll();
        sliderCoroutine ??= StartCoroutine(SliderUpdateLoop());
        countdownCoroutine ??= StartCoroutine(CountdownUpdateLoop());
    }

    private void HandleRefill()
    {
        RefreshAll();
        if (sliderCoroutine != null)
        {
            StopCoroutine(sliderCoroutine);
            sliderCoroutine = null;
        }
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
        }
        startCounting = false;
    }

    private IEnumerator SliderUpdateLoop()
    {
        while (true)
        {
            if (manager.GetSecondsUntilNextBarFull() <= 0f)
            {
                UpdateSliders();
                sliderCoroutine = null;
                yield break;
            }
            UpdateSliders();
            yield return sliderWaitSeconds;
        }
    }

    private IEnumerator CountdownUpdateLoop()
    {
        while (true)
        {
            float secs = manager.GetSecondsUntilNextBarFull();
            if (secs <= 0f)
            {
                triesTimeCounter.text = "Tries Complete";
                countdownCoroutine = null;
                yield break;
            }
            UpdateCountdownText();
            yield return waitForSeconds;
        }
    }

    private void RefreshAll()
    {
        UpdateSliders();
        UpdateCountdownText();
    }

    private void UpdateSliders()
    {
        int maxBars = manager.GetMaxBars();
        int current = manager.CurrentTriesNumber;
        float progressFraction = manager.GetProgressFraction();
        int displayCount = Mathf.Min(triesBarArray.Length, maxBars);

        for (int i = 0; i < displayCount; ++i)
        {
            var s = triesBarArray[i];
            if (i < current)
            {
                s.value = 1f;
            }
            else if (i == current)
            {
                s.value = Mathf.Clamp01(progressFraction);
            }
            else
            {
                s.value = 0f;
            }
        }
        for (int i = displayCount; i < triesBarArray.Length; ++i)
        {
            triesBarArray[i].value = 0f;
        }
    }

    private void UpdateCountdownText()
    {
        float secs = manager.GetSecondsUntilNextBarFull();
        if (secs <= 0f)
        {
            triesTimeCounter.text = "Tries Complete";
            return;
        }
        int total = Mathf.CeilToInt(secs);
        int minutes = total / 60;
        int seconds = total % 60;
        triesTimeCounter.text = $"Time Left To Fill: {minutes:D2}:{seconds:D2}";
    }
}
